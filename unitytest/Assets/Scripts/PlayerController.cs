using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int JumpPower;

    private float Gravity = -9.81f;
    private float GravityMultiplier = 3.0f;
    private float DebounceTimerf = 1.0f;
    public bool DamageImmunity;
    public bool Swinging = false;
    public bool Invisible;
    public int Health;

    private float Velocity;

    private Vector2 Direction;
    private Animator Anim;
    public GameObject Claws;

    public float Speed;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        Anim = Claws.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void ApplyMovement()
    {
        if (Health > 0)
        {
            var X = 0;
            if (Input.GetKey(KeyCode.D))
            {
                X += 1;
            };

            if (Input.GetKey(KeyCode.A))
            {
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

            Direction.x = X;
        }
        controller.Move(Direction * Speed * Time.deltaTime);

    }

    private void Swing()
    {
        if (Input.GetButtonDown("Fire1") == true && Health > 0)
        {
            if (Swinging == true) return;
            Swinging = true;
            Anim.Play("Slash", 0, 0.0f);
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
        if (!controller.isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space) == true && Health > 0)
        {

            Velocity += JumpPower;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Direction = new Vector2(0, 0);
        Swing();
        ApplyGravity();
        Jump();
        ApplyMovement();
    }

    IEnumerator DebounceTimer(string str)
    {
        yield return new WaitForSecondsRealtime(DebounceTimerf);
        if (str == "Swing")
        {
            Swinging = false;
        }
        if (str == "Damage")
        {
            yield return new WaitForSecondsRealtime(2.0f);
            DamageImmunity = false;
        }
    }

    IEnumerator CloakTimer()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        Invisible = false;
    }

    void DealDamage()
    {
        if (DamageImmunity == true) return;
        Health -= 1;
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

    void OnTriggerEnter(Collider collision)
    {
        if (Health > 0)
        {
            switch (collision.tag)
            {
                case "Heart":
                    Health += 1;
                    Destroy(collision.gameObject);
                    break;

                case "Projectile":
                    if (collision.gameObject.GetComponent<SpriteRenderer>().color.a > 0.5f) 
                    {
                        DealDamage();
                    }
                    break;

                case "Spike":
                    DealDamage();
                    break;

                case "Enemy":
                    DealDamage();
                    break;

                case "Cloak":
                    Invisible = true;
                    Destroy(collision.gameObject);
                    StartCoroutine(CloakTimer());
                    break;

                case "Claw":
                    Destroy(collision.gameObject);
                    gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size = new Vector3(gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.x + 0.2f, gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.y, gameObject.transform.Find("Claws").GetComponent<BoxCollider>().size.z);
                    gameObject.transform.Find("Claws").GetComponent<BoxCollider>().center = new Vector3(gameObject.transform.Find("Claws").GetComponent<BoxCollider>().center.x + 0.1f, 0.0f, 0.0f);
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
