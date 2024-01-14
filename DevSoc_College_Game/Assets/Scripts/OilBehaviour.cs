using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBehaviour : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject explosionFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health < 0)
        {

            GameObject.Instantiate(explosionFire, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttackKnife")
        {
            if (health > 0)
            {
                health -= 35f;
            }

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "PlayerAttackBullet")
        {
            if (health > 0)
            {
                health -= 15f;
            }
        }
    }
}
