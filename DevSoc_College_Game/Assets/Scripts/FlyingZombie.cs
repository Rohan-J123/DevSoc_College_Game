using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingZombie : MonoBehaviour
{
    public Transform player;

    public LayerMask isPlayer;

    public Vector3 FlyPoint;
    bool FlyPointSet;
    public float FlyPointRange;
    bool inSightRange, inAttackRange;
    public float sightRange, attackRange;
    public float timeBetweenFly;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        if (!FlyPointSet)
        {
            SearchFlyPoint();
        }
        if (FlyPointSet)
        {
            gameObject.transform.position = FlyPoint;
        }

        Vector3 distanceWalkPoint = transform.position - FlyPoint;

        if (distanceWalkPoint.magnitude < timeBetweenFly)
        {
            FlyPointSet = false;
        }
    }

    private void SearchFlyPoint()
    {
        float FlyPointZ = UnityEngine.Random.Range(-FlyPointRange, FlyPointRange);
        float FlyPointX = UnityEngine.Random.Range(-FlyPointRange, FlyPointRange);
        float FlyPointY = UnityEngine.Random.Range(-FlyPointRange, FlyPointRange);

        FlyPoint = new Vector3(transform.position.x + FlyPointX, transform.position.y + FlyPointY , transform.position.z + FlyPointZ);

    }
}
