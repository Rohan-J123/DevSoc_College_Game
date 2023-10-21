using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class PlayerAttack : MonoBehaviour
{
    private Animator selfAnim;
    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject assault;

    [SerializeField] private GameObject bullet;
    [SerializeField] private bool isShootingPistol = false;
    [SerializeField] private bool isShootingAssault = false;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private Rig aimRig;
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
            assault.SetActive(false);
        }
        if (starterAssetsInputs.pistol) {
            knife.SetActive(false);
            pistol.SetActive(true);
            assault.SetActive(false);
        }
        if (starterAssetsInputs.assault) {
            knife.SetActive(false);
            pistol.SetActive(false);
            assault.SetActive(true);
        }

        if (starterAssetsInputs.aim || starterAssetsInputs.attack) {
            if(knife.activeSelf) {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

                selfAnim.SetBool("isAttacking", true);
                selfAnim.SetBool("isPistolShooting", false);
                selfAnim.SetBool("isAiming", false);
                selfAnim.SetBool("isAssaultShooting", false);

                aimRig.weight = 0f;
            }
            else if (pistol.activeSelf){
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y - 15f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isAssaultShooting", false);

                aimRig.weight = 1f;
                if (starterAssetsInputs.attack) {
                    selfAnim.SetBool("isPistolShooting", true);
                    selfAnim.SetBool("isAiming", false);

                    if(isShootingPistol == false){
                        isShootingPistol = true;
                        StartCoroutine(shootingPistol());
                    }  
                }
                else {
                    selfAnim.SetBool("isPistolShooting", false);
                    selfAnim.SetBool("isAiming", true);
                }
            }
            else if (assault.activeSelf){
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y + 52f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isPistolShooting", false);
                selfAnim.SetBool("isAiming", false);
                selfAnim.SetBool("isAssaultShooting", true);

                aimRig.weight = 0f;
                StartCoroutine(shootingPistol());
                // if (starterAssetsInputs.attack) {
                //     selfAnim.SetBool("isPistolShooting", true);
                //     selfAnim.SetBool("isAiming", false);

                //     if(isShooting == false){
                //         isShooting = true;
                //         StartCoroutine(shooting());
                //     }  
                // }
                // else {
                //     selfAnim.SetBool("isPistolShooting", false);
                //     selfAnim.SetBool("isAiming", true);
                // }
            }
        }
        else {
            aimRig.weight = 0f;
            selfAnim.SetBool("isAttacking", false);
            selfAnim.SetBool("isPistolShooting", false);
            selfAnim.SetBool("isAiming", false);
            selfAnim.SetBool("isAssaultShooting", false);
        }
    }

    IEnumerator shootingPistol(){
        yield return new WaitForSeconds(0.6f);
        aimRig.weight = 0f;
        if (starterAssetsInputs.attack) {
            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));
        }
        aimRig.weight = 1f;
        isShootingPistol = false;
    }

    IEnumerator shootingAssault(){
        yield return new WaitForSeconds(0.1f);
        // aimRig.weight = 0f;
        if (starterAssetsInputs.attack) {
            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));
        }
        // aimRig.weight = 1f;
        isShootingAssault = false;
    }
}
