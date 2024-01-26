using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class crawlingZombie : MonoBehaviour
{    
    [SerializeField] private Animator animator;

    public NavMeshAgent agent;

    public Transform body; 

    public Transform player;

    public LayerMask isGround,isPlayer,isBody;

    //used for patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    bool inSightRange,inAttackRange;
    public float sightRange,attackRange,bodysightRange;
    public bool isbodyrange;
    public float canEatRange;

    //used for attacking
    public float timeBetweenAttack;
    public float timeBetweenWalk;
    bool attacked;

    private void Awake(){
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    private void Update(){
        //check for sight and attack range
        inSightRange = Physics.CheckSphere(transform.position,sightRange,isPlayer);
        inAttackRange = Physics.CheckSphere(transform.position,attackRange,isPlayer);
        isbodyrange = Physics.CheckSphere(transform.position,bodysightRange,isBody);

        if (inSightRange && !inAttackRange){
            Chase();
        }
        if(inSightRange && inAttackRange){
            Attack();
        }
        if(!inSightRange && !inAttackRange && isbodyrange){
            Eatbody();
        }
    }

    private void Eatbody()
    {
        agent.SetDestination(body.position);
        animator.SetBool("IsBiting", Physics.CheckSphere(transform.position, canEatRange, isBody));
    }

    private void Attack()
    {
        animator.SetBool("IsAttacking",true);
        animator.SetBool("IsCrawling",false);

        if(!attacked){
            attacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttack);
        }

        agent.SetDestination(transform.position);
        transform.LookAt(player);
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    private void Attacked()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<ZombieScript>().enabled = true;
        animator.SetBool("IsAttacked",false);
        animator.SetBool("IsCrawling",true);
    }

    private void Chase()
    {
        animator.SetBool("IsCrawling",true);
        animator.SetBool("IsAttacking",false);

       agent.SetDestination(player.position);
    }
}
