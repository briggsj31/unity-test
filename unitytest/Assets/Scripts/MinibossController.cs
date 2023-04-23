using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossController : MonoBehaviour
{
    public int Health;

    public bool Attacking = false;


    public Vector2 Direction = Vector2.zero;

    public GameObject PlayerEntered;
    private Animator Anim;
    public GameObject Claws;

    private GameObject Stone;
    private GameObject StoneThrow;

    CharacterController controller;

    void Start()
    {
        Anim = Claws.GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        Stone = gameObject.transform.Find("Stone").gameObject;
        Stone.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        Rotate();
        Attack();
    }

    IEnumerator DebounceTimer(string Inp)
    {
        if (Inp == "Throw") {
            yield return new WaitForSecondsRealtime(2.0f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.8f);
        }
        

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
        if (Attacking == false && Direction != Vector2.zero && PlayerEntered != null)
        {
            Attacking = true;
            float distance = (gameObject.transform.position - PlayerEntered.transform.position).magnitude;
            Debug.Log(distance);
            if (distance < 6.0f)
            {
                Anim.Play("Slash", 0, 0.0f);
                StartCoroutine(DebounceTimer("Swing"));
            }
            else
            {
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
                StartCoroutine(DebounceTimer("Throw"));
            }
            


            
        }

    }
}
