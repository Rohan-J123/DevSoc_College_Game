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
        speed = 30f;
        bulletRB = GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * speed;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col){
        Destroy(gameObject);
    }
}
