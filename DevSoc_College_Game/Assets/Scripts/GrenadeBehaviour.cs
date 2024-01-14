using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject explosionFire;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(des());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Instantiate(explosionFire, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator des()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Instantiate(explosionFire, transform.position, transform.rotation);
    }
}
