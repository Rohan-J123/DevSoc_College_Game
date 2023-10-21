using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private GameObject laserPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        laserPoint = GameObject.FindGameObjectWithTag("LaserPoint");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit rayCastHit, 1000f, aimColliderLayerMask)){
            laserPoint.transform.position = rayCastHit.point;
        }
    }
}
