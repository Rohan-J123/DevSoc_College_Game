using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    private Animator selfAnim;
    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject pistol;

    [SerializeField] private GameObject bullet;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private Transform bulletSpawnPoint;

    public Vector3 mouseWorldPosition;

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
                selfAnim.SetBool("isAiming", false);
            }
            else if (pistol.activeSelf){
                selfAnim.SetBool("isAttacking", false);

                if (starterAssetsInputs.attack) {
                    selfAnim.SetBool("isShooting", true);
                    selfAnim.SetBool("isAiming", false);

                    if(isShooting == false){
                        isShooting = true;
                        StartCoroutine(shooting());
                    }  
                }
                else {
                    selfAnim.SetBool("isShooting", false);
                    selfAnim.SetBool("isAiming", true);
                }

            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else {
            selfAnim.SetBool("isAttacking", false);
            selfAnim.SetBool("isShooting", false);
            selfAnim.SetBool("isAiming", false);
        }
    }

    IEnumerator shooting(){
        yield return new WaitForSeconds(0.6f);
        Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));
        isShooting = false;
    }
}
