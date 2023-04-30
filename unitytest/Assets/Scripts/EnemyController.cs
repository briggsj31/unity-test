using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Health;
    public int JumpPower;

    private float Gravity = -9.81f;
    private float GravityMultiplier = 3.0f;
    private float DebounceTimerf = 1.0f;
    public bool DamageImmunity;
    public bool Swinging;
    public bool Invisible;
    private int Speed = 4;

    public GameObject PlayerEntered;

    private float Velocity;

    public Vector2 Direction = Vector2.zero;
    private Animator Anim;

    CharacterController controller;

    void Start()
    {
        //Anim = Claws.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    IEnumerator DebounceTimer()
    {
        yield return new WaitForSecondsRealtime(DebounceTimerf);
    }

    private void ApplyMovement()
    {

        if (Direction.x == 1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        }
        if (Direction.x == -1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
        }

        controller.Move(Direction * Speed * Time.deltaTime);

       
    }

    private void Attack()
    {
        // Anim.Play("Slash", 0, 0.0f);
        Swinging = true;
        StartCoroutine(DebounceTimer());
        Swinging = false;
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
        Velocity += JumpPower;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (Health > 0)
        {
        }
    }
}
