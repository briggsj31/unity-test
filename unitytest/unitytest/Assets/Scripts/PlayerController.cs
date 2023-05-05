using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int JumpPower;

    private float Gravity = -9.81f;
    private float GravityMultiplier = 2.0f;
    private int CollectedMeat = 0;
    private float DebounceTimerf = 0.5f;
    public int Wolfsbane = 0;
    public bool DamageImmunity;
    public bool Swinging = false;
    public bool Invisible;
    private bool LongClaws = false;
    public int Health;
    public Canvas UserInterface;

    private float Velocity;

    private Vector2 Direction;
   // private Animator Anim;
    private Animator CharlieAnim; 
    public GameObject Claws;
    public GameObject Body;

    public float Speed;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //Anim = Claws.GetComponent<Animator>();
        CharlieAnim = Body.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void ApplyMovement()
    {
        if (Health > 0)
        {
            var X = 0;
            if (Input.GetKey(KeyCode.D))
            {
                CharlieAnim.SetBool("IsMoving", true);
                X += 1;
            };

            if (Input.GetKey(KeyCode.A))
            {
                CharlieAnim.SetBool("IsMoving", true);
                X -= 1;
            };

            if (X == 1)
            {
                gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            }
            if (X == -1)
            {
                gameObject.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
            }
            if (X == 0)
            {
                CharlieAnim.SetBool("IsMoving", false);
            }

            Direction.x = X;
        }
        controller.Move(Direction * Speed * Time.deltaTime);

    }

    private void Swing()
    {
        if (Input.GetButtonDown("Fire1") == true && Health > 0)
        {
            if (Swinging == true) return;
            if (gameObject.transform.Find("Claws").gameObject.GetComponent<MeleeHitbox>().EnemyToDamage != null)
            {
                GameObject ControllerRef = gameObject.transform.Find("Claws").gameObject.GetComponent<MeleeHitbox>().EnemyToDamage;
                if (ControllerRef.GetComponent<EnemyController>() != null)
                {
                    if (ControllerRef.GetComponent<EnemyController>().Health == 0)
                    {

                        Destroy(ControllerRef, 1);
                    }
                    else
                    {
                        ControllerRef.GetComponent<EnemyController>().Health -= 1;
                    }
                }
                if (ControllerRef.GetComponent<ThrowerEnemyController>() != null)
                {
                    if (ControllerRef.GetComponent<ThrowerEnemyController>().Health == 0)
                    {
                        Destroy(ControllerRef, 1);
                    }
                    else
                    {
                        ControllerRef.GetComponent<ThrowerEnemyController>().Health -= 1;
                    }
                }
                if (ControllerRef.GetComponent<MinibossController>() != null)
                {
                    if (ControllerRef.GetComponent<MinibossController>().Health == 0)
                    {
                        Destroy(ControllerRef, 1);
                    }
                    else
                    {
                        ControllerRef.GetComponent<MinibossController>().Health -= 1;
                    }
                }
            }
            Swinging = true;
            gameObject.transform.Find("ClawSwipe").gameObject.GetComponent<AudioSource>().Play(0);
            CharlieAnim.SetTrigger("Attack");
            //Anim.Play("Slash", 0, 0.0f);
            StartCoroutine(DebounceTimer("Swing"));
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && Velocity < 0.0f)
        {
            Velocity = -1.0f;
        }
        else
        {
            Velocity += Gravity * GravityMultiplier * Time.deltaTime;
        };

        Direction.y = Velocity;
    }

    private void Jump()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && Health > 0)
        {

            Velocity += JumpPower;
            CharlieAnim.SetTrigger("Jump");
            gameObject.transform.Find("Jump").gameObject.GetComponent<AudioSource>().Play(0);

        }
    }

    private void Animate()
    {
        if (Swinging == false)
        {
            if (LongClaws == true) {

            }
        }
    }

    private bool JumpSound = false;
    private bool WalkSound = false;

    // Update is called once per frame
    void Update()
    {
        Direction = new Vector2(0, 0);
        Swing();
        ApplyGravity();
        Jump();
        ApplyMovement();
        if (controller.isGrounded)
        {
            if (JumpSound == true)
            {
                JumpSound = false;
                CharlieAnim.ResetTrigger("Jump");
            }
            if (Direction.x != 0 && WalkSound == false)
            {
                WalkSound = true;
                gameObject.transform.Find("WalkingSound").gameObject.GetComponent<AudioSource>().Play(0);
            }
            
        }
        if (!controller.isGrounded)
        {
            if (JumpSound == false)
            {
                JumpSound = true;
            }
            gameObject.transform.Find("WalkingSound").gameObject.GetComponent<AudioSource>().Pause();
            WalkSound = false;
        }
    }

    IEnumerator DebounceTimer(string str)
    {
        yield return new WaitForSecondsRealtime(DebounceTimerf);
        if (str == "Swing")
        {
            Swinging = false;
            CharlieAnim.ResetTrigger("Attack");
        }
        if (str == "Damage")
        {
            yield return new WaitForSecondsRealtime(1.0f);
            CharlieAnim.ResetTrigger("Injured");
            yield return new WaitForSecondsRealtime(1.5f);
            DamageImmunity = false;
        }
    }

    IEnumerator CloakTimer()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        Invisible = false;
        CharlieAnim.SetBool("Invisible", false);
    }

    void DealDamage(string str)
    {
        if (str == "InstaKill")
        {
            Health = 0;
            CharlieAnim.SetTrigger("Injured");
            UserInterface.GetComponent<DeathScreen>().ChangeHealth(Health);
            gameObject.transform.Find("r").GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            if (DamageImmunity == true || Invisible == true) return;
            Health -= 1;
            if (Health == 1)
            {
                gameObject.transform.Find("LowHealth").gameObject.GetComponent<AudioSource>().Play(0);
            }
            if (Health >= 2)
            {
                gameObject.transform.Find("TakeDamage").gameObject.GetComponent<AudioSource>().Play(0);
            }
            UserInterface.GetComponent<DeathScreen>().ChangeHealth(Health);
            CharlieAnim.SetBool("Claws", false);
            LongClaws = false;
            CharlieAnim.SetTrigger("Injured");
            if (Health == 0)
            {
                gameObject.transform.Find("r").GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                DamageImmunity = true;
                StartCoroutine(DebounceTimer("Damage"));
            }
        }
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (Health > 0)
        {
            switch (collision.tag)
            {
                case "Heart":
                    Health += 1;
                    gameObject.transform.Find("HeartCollection").gameObject.GetComponent<AudioSource>().Play(0);
                    UserInterface.GetComponent<DeathScreen>().ChangeHealth(Health);
                    Destroy(collision.gameObject);
                    break;

                case "Projectile":
                    if (collision.gameObject.GetComponent<SpriteRenderer>().color.a > 0.5f) 
                    {
                        DealDamage("");
                    }
                    break;

                case "Spike":
                    DealDamage("");
                    break;

                case "Enemy":
                    DealDamage("");
                    break;

                case "Cloak":
                    Invisible = true;
                    gameObject.transform.Find("CloakCollect").gameObject.GetComponent<AudioSource>().Play(0);
                    CharlieAnim.SetBool("Invisible", true);
                    Destroy(collision.gameObject);
                    StartCoroutine(CloakTimer());
                    break;

                case "Claw":
                    if (LongClaws == false)
                    {
                        gameObject.transform.Find("ClawPowerup").gameObject.GetComponent<AudioSource>().Play(0);
                        CharlieAnim.SetBool("Claws", true);
                        Destroy(collision.gameObject);
                        gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size = new Vector3(gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.x + 0.2f, gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.y, gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.z);
                        gameObject.transform.Find("Claws").GetComponent<BoxCollider>().center = new Vector3(gameObject.transform.Find("Claws").GetComponent<BoxCollider>().center.x + 0.1f, 0.0f, 0.0f);
                        LongClaws = true;
                    }
        
                    break;

                case "Meat":
                    Destroy(collision.gameObject);
                    CollectedMeat += 1;
                    if (CollectedMeat >= 10)
                    {
                        if (Health < 3)
                        {
                            Health += 1;
                            CollectedMeat -= 10;
                            
                        }
                    }
                    break;

                case "Wolfsbane":
                    gameObject.transform.Find("WolfsbanePickup").gameObject.GetComponent<AudioSource>().Play(0);
                    Destroy(collision.gameObject);
                    Wolfsbane += 1;
                    break;

                case "Water":
                    gameObject.transform.Find("LowHealth").gameObject.GetComponent<AudioSource>().Play(0);
                    DealDamage("InstaKill");
                    break;

                case "Sewer":
                    gameObject.transform.Find("LowHealth").gameObject.GetComponent<AudioSource>().Play(0);
                    DealDamage("InstaKill");
                    break;


                default:
                    break;

            }

            if (Health > 3)
            {
                Health = 3;
            }
        }
    }

}
