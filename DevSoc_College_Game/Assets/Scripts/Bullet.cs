using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRB;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 40f;
        bulletRB = GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col){
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col){
        Destroy(gameObject, 0.05f);
    }
}
