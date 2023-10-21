using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    private Animator selfAnim;
    private StarterAssetsInputs starterAssetsInputs;

    public GameObject knife;
    public GameObject pistol;
    // Start is called before the first frame update
    void Start()
    {
        selfAnim = GetComponent<Animator>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.knife) {  
            knife.SetActive(true);
            pistol.SetActive(false);
        }
        if (starterAssetsInputs.pistol) {
            knife.SetActive(false);
            pistol.SetActive(true);
        }

        if (starterAssetsInputs.aim || starterAssetsInputs.attack) {
            if(knife.activeSelf) {
                selfAnim.SetBool("isAttacking", true);
                selfAnim.SetBool("isShooting", false);
            }
            else if (pistol.activeSelf){
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isShooting", true);
            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else {
            selfAnim.SetBool("isAttacking", false);
            selfAnim.SetBool("isShooting", false);
        }
    }
}
