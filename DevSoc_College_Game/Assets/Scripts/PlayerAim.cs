using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAim : MonoBehaviour
{
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
