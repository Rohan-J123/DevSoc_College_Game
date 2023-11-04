using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    public GameObject healthbar; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health > 0)
        {
            healthbar.transform.localScale = new Vector3((health / 100f), 1f, 1f);
        }
        else
        {
            healthbar.transform.localScale = new Vector3(0f, 1f, 1f);
            GameObject boneHip = transform.parent.gameObject;
            GameObject zombie = boneHip.transform.parent.gameObject;
            Animator zombieAnim = zombie.GetComponent<Animator>();
            zombieAnim.SetBool("isKilled", true);
            Destroy(zombie, 4f);
        } 
    }
    void OnTriggerEnter(Collider col){
        if(col.tag == "PlayerAttackKnife"){
            if(health > 0)
            {
                health -= 35f;
            }
            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "PlayerAttackBullet")
        {
            if(health > 0)
            {
                health -= 15f;
            }
        }
    }
}
