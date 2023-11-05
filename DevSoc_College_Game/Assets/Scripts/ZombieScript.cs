using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public static ZombieScript instance;      
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
        instance = this;
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

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
        animator.SetBool("isAttacked",false);

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
        animator.SetBool("isAttacked",false);
        
        if(!walkPointSet){
            SearchWalkPoint();
        }
        if(walkPointSet){
            agent.SetDestination(walkPoint);
            ResetWalk();
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        if(distanceWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }

    IEnumerator ResetWalk(){
        animator.SetBool("IsIdle",true);
        yield return new WaitForSeconds(1);
        animator.SetBool("IsIdle",false);
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
        animator.SetBool("isAttacked",false);

        agent.SetDestination(player.position);
    }

    
}
