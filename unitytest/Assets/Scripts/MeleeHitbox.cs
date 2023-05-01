using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour 
{

    private GameObject ControllerRef;
    public bool CanDamage = false;
    public GameObject EnemyToDamage;
    // Start is called before the first frame update
    void Start()
    {
        EnemyToDamage = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyToDamage = collision.gameObject;
            /*
            if (gameObject.transform.parent.gameObject.GetComponent<PlayerController>().Swinging == true)
            {
                ControllerRef = collision.gameObject;
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
                CanDamage = true;



            } */
        }
    }


    void OnTriggerLeave(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.gameObject == EnemyToDamage)
            {
                CanDamage = false;
                EnemyToDamage = null;
            }
        }
    }
}
