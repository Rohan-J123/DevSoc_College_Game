using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float health = 100f;
    public GameObject healthbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthbar.transform.localScale = new Vector3((health/100f), 1f, 1f);
    }

    void onCollisionEnter(Collider col){
        if(col.tag == "PlayerAttackKnife"){
            health -= 35f;
        }
    }
}
