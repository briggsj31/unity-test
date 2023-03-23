using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private GameObject LocalEnemy;
    private float X;

    private GameObject PlayerToMoveTo;
    private bool Detected = false;

    // Start is called before the first frame update
    void Start()
    {
        LocalEnemy = gameObject.transform.parent.gameObject;
    }

    // Update is called once per fram
    void Update()
    {
        if (Detected == true)
        {
            if (PlayerToMoveTo.GetComponent<PlayerController>().Invisible == false)
            {
                X = (gameObject.transform.parent.Find("r").position.x - PlayerToMoveTo.transform.Find("r").position.x);
                if (X > 0)
                {
                    X = 1;
                }
                if (X < 0)
                {
                    X = -1;
                }
                LocalEnemy.GetComponent<EnemyController>().Direction = new Vector2(-X, 0);
            }
        }
        else
        {
            LocalEnemy.GetComponent<EnemyController>().Direction = Vector2.zero;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerToMoveTo = collider.gameObject;
            Detected = true;
        }
    }

    void OnTriggerLeave(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Detected = false; 
        }
    }
}