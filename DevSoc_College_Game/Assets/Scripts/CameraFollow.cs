using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform self;
    private GameObject cameraFollow;
    private Transform cameraFollowGrenade;
    private GameObject player;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraFollow = GameObject.FindGameObjectWithTag("CameraFollow");
        cameraFollowGrenade = cameraFollow.transform.Find("CameraFollowGrenade");
        starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.grenade && !starterAssetsInputs.attack && !starterAssetsInputs.aim)
        {
            
            self.position = cameraFollowGrenade.position;
        }
        else
        {
            self.position = cameraFollow.transform.position;
        }
    }
}
