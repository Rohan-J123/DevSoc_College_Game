using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject laserPoint;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    private StarterAssetsInputs starterAssetsInputs;
    private bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        laserPoint = GameObject.FindGameObjectWithTag("LaserPoint");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        isAiming = false;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit rayCastHit, 1000f, aimColliderLayerMask)){
            playerAttack.mouseWorldPosition =  rayCastHit.point;
            laserPoint.transform.position =  rayCastHit.point;
        }

        if(starterAssetsInputs.aim){
            if(!isAiming){
                isAiming = true;
                // cameraFollow.transform.position = mainCamera.transform.position + new Vector3(0f, 0f, 1.5f);
            }
        }
        else{
            isAiming = false;
            // cameraFollow.transform.position = mainCamera.transform.position + new Vector3(0f, 0f, -1.5f);
        }
    }
}
