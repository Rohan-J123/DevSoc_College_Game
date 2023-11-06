using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform self;
    private GameObject player;
    private GameObject cameraFollow;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraFollow = GameObject.FindGameObjectWithTag("CameraFollow");
        starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.grenade)
        {
            self.position = cameraFollow.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        else
        {
            self.position = cameraFollow.transform.position;
        }
    }
}
