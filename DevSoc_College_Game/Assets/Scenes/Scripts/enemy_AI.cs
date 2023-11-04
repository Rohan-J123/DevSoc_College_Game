using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_AI : MonoBehaviour
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
    bool attacked;

    private void Awake(){
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        //check for sight and attack range
        inSightRange = Physics.CheckSphere(transform.position,sightRange,isPlayer);
        inAttackRange = Physics.CheckSphere(transform.position,attackRange,isPlayer);

        if(inSightRange && !inAttackRange){
            Chase();
        }
        if(!inSightRange && !inAttackRange){
            Patrol();
        }
        if(inSightRange && inAttackRange){
            Attack();
        }
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

    private void Patrol()
    {
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsAttacking",false);
        
        if(!walkPointSet){
            SearchWalkPoint();
        }
        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        if(distanceWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float walkPointZ = UnityEngine.Random.Range(-walkPointRange,walkPointRange);
        float walkPointX = UnityEngine.Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + walkPointX , transform.position.y,transform.position.z + walkPointZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,isGround)){
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsAttacking",false);

        agent.SetDestination(player.position);
    }
}
