using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour 
{

    private GameObject ControllerRef;
    private bool Destroying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            if (gameObject.transform.parent.GetComponent<PlayerController>().Swinging == true)
            {
                ControllerRef = collision.gameObject;
                if (ControllerRef.GetComponent<EnemyController>() != null)
                {
                    if (ControllerRef.GetComponent<EnemyController>().Health == 0)
                    {
                        if (Destroying == false)
                        {
                            Destroying = true;
                            Destroy(collision.gameObject, 1);
                        }
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
                        if (Destroying == false)
                        {
                            Destroying = true;
                            Destroy(collision.gameObject, 1);
                        }
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
                        if (Destroying == false)
                        {
                            Destroying = true;
                            Destroy(collision.gameObject, 1);
                        }
                    }
                    else
                    {
                        ControllerRef.GetComponent<MinibossController>().Health -= 1;
                    }
                }


                
            }
        }
    }
}
