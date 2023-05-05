using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag != "Enemy" && collider.tag != "EnemyDetection" && collider.tag != "Projectile" && gameObject.GetComponent<SpriteRenderer>().color.a > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
