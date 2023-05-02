using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerEnemyController : MonoBehaviour
{
    public int Health;

    private float DebounceTimerf = 2.5f;
    public bool Attacking = false;


    public Vector2 Direction = Vector2.zero;

    public GameObject PlayerEntered;


    private GameObject Stone;
    private GameObject StoneThrow;

    private Animator Anim;

    CharacterController controller;

    void Start()
    {
        Anim = gameObject.transform.Find("r").gameObject.GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        Stone = gameObject.transform.Find("Stone").gameObject;
        Stone.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        Rotate();
        Attack();
    }

    IEnumerator DebounceTimer()
    {
        yield return new WaitForSecondsRealtime(DebounceTimerf);
        Anim.ResetTrigger("Attack");
        Attacking = false;
    }

    private void Rotate()
    {
        if (Direction.x == 1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        }
        if (Direction.x == -1)
        {
            gameObject.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
        }
    }

    private void Attack()
    {
        if (Attacking == false && Direction != Vector2.zero)
        {
            Anim.SetTrigger("Attack");
            Attacking = true;
            StoneThrow = Instantiate(Stone);
            StoneThrow.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            StoneThrow.transform.position = Stone.transform.position;
            if (gameObject.name == "Enemy_Thrower")
            {
                StoneThrow.GetComponent<Rigidbody>().AddForce(transform.right * 400);
            }
            else
            {
                StoneThrow.GetComponent<Rigidbody>().AddForce((transform.right + new Vector3(0, (transform.position - PlayerEntered.transform.position).magnitude / 13, 0)) * 400);
                StoneThrow.GetComponent<Rigidbody>().useGravity = true;
            }
            
            
            StartCoroutine(DebounceTimer());
        }
        
    }


    void OnTriggerEnter(Collider collision)
    {
        if (Health > 0)
        {
        }
    }
}
