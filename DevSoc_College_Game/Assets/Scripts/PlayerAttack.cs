using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator selfAnim;

    public GameObject knife;
    public GameObject pistol;
    // Start is called before the first frame update
    void Start()
    {
        selfAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {  
            knife.SetActive(true);
            pistol.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            knife.SetActive(false);
            pistol.SetActive(true);
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
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
