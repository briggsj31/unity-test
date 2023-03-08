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
            ControllerRef = collision.gameObject;
            if (ControllerRef.GetComponent<EnemyController>().Health == 0)
            {
                if (Destroying == false)
                {
                    Destroying = true;
                    Destroy(collision.gameObject, 1);
                }
                else
                {
                    ControllerRef.GetComponent<EnemyController>().Health -= 1;
                    Debug.Log("Enemy Hit!");
                }

            }
        }
    }
}
