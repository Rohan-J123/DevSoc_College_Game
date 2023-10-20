using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform self;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("CameraFollow").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        self.position = target.position;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f){
                Time.timeScale = 1f;
            }
            else{
                Time.timeScale = 0f;
            }
        }
    }
}
