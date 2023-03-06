using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int JumpPower;

    private float Gravity = -9.81f;
    private float GravityMultiplier = 3.0f;
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
        var X = 0;
        if (Input.GetKey(KeyCode.D))
        {
            X += 1;
        };

        if (Input.GetKey(KeyCode.A))
        {
            X -= 1;
        };

        Direction.x = X;
        controller.Move(Direction * Speed * Time.deltaTime);
    }

    private void Swing()
    {
        if (Input.GetButtonDown("Fire1") == true)
        {
            Debug.Log("Swinging");
            Anim.Play("Slash", 0, 0.0f);
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

        if (Input.GetKeyDown(KeyCode.Space) == true)
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
}
