using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crawlingZombie : MonoBehaviour
{    
    [SerializeField] private Animator animator;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask isGround,isPlayer;

    //used for patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    bool inSightRange,inAttackRange;
    public float sightRange,attackRange;

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

        if(inSightRange && !inAttackRange){
            Chase();
        }
        if(inSightRange && inAttackRange){
            Attack();
        }
        if(!inSightRange && !inAttackRange){
            Eatbody();
        }
    }

    private void Eatbody()
    {

    }

    private void Attack()
    {
        animator.SetBool("IsAttacking",true);
        animator.SetBool("IsWalking",false);

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!attacked){
            attacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttack);
        }
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
        animator.SetBool("IsWalking",true);
    }

    private void Chase()
    {
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsAttacking",false);

        agent.SetDestination(player.position);
    }
}
