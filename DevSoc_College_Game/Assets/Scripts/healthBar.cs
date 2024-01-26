using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class healthBar : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject zombie;
    public GameObject healthbar;
    public float WaitTime;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health > 0)
        {
            healthbar.transform.localScale = new Vector3((health / 100f), 1f, 1f);
        }
        else
        {
            healthbar.transform.localScale = new Vector3(0f, 1f, 1f);
            animator.SetBool("IsKilled", true);
            animator.SetBool("IsAttacked",false);
            zombie.GetComponent<NavMeshAgent>().enabled = false;
            zombie.GetComponent<ZombieScript>().enabled = false;
            Destroy(zombie,4f);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttackKnife")
        {
            animator.SetBool("IsAttacked",true);
            zombie.GetComponent<NavMeshAgent>().enabled = false;
            zombie.GetComponent<ZombieScript>().enabled = false;
            Invoke("ResetNav", WaitTime);
            if (health > 0)
            {
                health -= 5f;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "PlayerAttackBullet")
        {
            animator.SetBool("IsAttacked",true);
            zombie.GetComponent<NavMeshAgent>().enabled = false;
            zombie.GetComponent<ZombieScript>().enabled = false;
            Invoke("ResetNav", WaitTime);
            if (health > 0)
            {
                health -= 15f;
            }
        }
    }

    private void ResetNav()
    {
        zombie.GetComponent<NavMeshAgent>().enabled = true;
        zombie.GetComponent<ZombieScript>().enabled = true;
    }
}
