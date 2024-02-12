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
    public bool isbodyrange = true;
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
        //isbodyrange = Physics.CheckSphere(transform.position, 400f, isBody);
        inSightRange = Physics.CheckSphere(transform.position,sightRange,isPlayer);
        inAttackRange = Physics.CheckSphere(transform.position,attackRange,isPlayer);


        if (inSightRange && !inAttackRange){
            Chase();
        }
        if(inSightRange && inAttackRange){
            Attack();
        }
        if (!inSightRange && !inAttackRange)
        {
            Patrol();
        }
        if(healthBar.attacked == true)
        {
            Attacked();
        }
    }

    private void Patrol()
    {
        animator.SetBool("IsCrawling", true);

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        if (distanceWalkPoint.magnitude < timeBetweenWalk)
        {
            walkPointSet = false;
        }
    }
    
    private void SearchWalkPoint()
    {
        float walkPointZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float walkPointX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + walkPointX, transform.position.y, transform.position.z + walkPointZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    private void Eatbody()
    {
        agent.SetDestination(body.position);
        animator.SetBool("IsBiting", Physics.CheckSphere(transform.position, canEatRange, isBody));
    }

    private void Attack()
    {
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
        agent.SetDestination(player.position);
    }

    private void Chase()
    {
       agent.SetDestination(player.position);
    }
}
