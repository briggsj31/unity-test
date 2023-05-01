using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossController : MonoBehaviour
{
    public int Health = 3;

    public bool Attacking = false;

    private float Gravity = -9.81f;
    private float GravityMultiplier = 3.0f;

    public Vector2 Direction = Vector2.zero;

    public GameObject PlayerEntered;
    private Animator Anim;
    private Animator ClawAnim;
    public GameObject SpriteBox;

    private GameObject Stone;
    private GameObject StoneThrow;

    private int Speed = 3;
    public int JumpPower;

    private float Velocity;

    private CharacterController controller;

    void Start()
    {
        Anim = SpriteBox.GetComponent<Animator>();
        ClawAnim = gameObject.transform.Find("Claws").gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        Stone = gameObject.transform.Find("Stone").gameObject;
        Stone.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        ApplyGravity();
        ApplyMovement();
        Attack();
    }

    IEnumerator DebounceTimer(string Inp)
    {
        if (Inp == "Throw") {
            yield return new WaitForSecondsRealtime(2.0f);
            Anim.ResetTrigger("Shoot");
        }
        else
        {
            yield return new WaitForSecondsRealtime(1.0f);
            Anim.ResetTrigger("Slash");
        }
        Attacking = false;
    }


    private void Attack()
    {
        if (Attacking == false && Direction != Vector2.zero && PlayerEntered != null)
        {
            Attacking = true;
            float distance = (gameObject.transform.position - PlayerEntered.transform.position).magnitude;
            Debug.Log(distance);
            if (distance < 6.0f)
            {
                Anim.SetTrigger("Slash");
                ClawAnim.Play("Slash", 0, 0.0f);
                StartCoroutine(DebounceTimer("Swing"));
            }
            else
            {
                Anim.SetTrigger("Shoot");
                StoneThrow = Instantiate(Stone);
                StoneThrow.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                StoneThrow.transform.position = Stone.transform.position;

                StoneThrow.GetComponent<Rigidbody>().AddForce(transform.right * 600);
                StartCoroutine(DebounceTimer("Throw"));
            }
        }

    }

    private void ApplyMovement()
    {

        if (Direction.x == 1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            Anim.SetBool("IsMoving", true);
        }
        if (Direction.x == -1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
            Anim.SetBool("IsMoving", true);
        }
        if (Direction.x == 0)
        {
            Anim.SetBool("IsMoving", false);
        }

        controller.Move(Direction * Speed * Time.deltaTime);
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
}
