using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject zombie;
    public GameObject healthbar;
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
            Destroy(zombie, 4f);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttackKnife")
        {
            animator.SetBool("IsAttacked",true);
            animator.SetBool("IsWalking",false);
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
            animator.SetBool("IsWalking",false);
            if (health > 0)
            {
                health -= 15f;
            }
        }
    }
}
