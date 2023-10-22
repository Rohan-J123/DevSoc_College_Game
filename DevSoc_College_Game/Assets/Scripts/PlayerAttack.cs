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
    [SerializeField] private Transform bulletSpawnPointPistol;
    [SerializeField] private Transform bulletSpawnPointAssault;

    [SerializeField] private Rig aimRigPistol;
    [SerializeField] private Rig aimRigAssault;
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
                selfAnim.SetBool("isAssaultAiming", false);

                aimRigPistol.weight = 0f;
                aimRigAssault.weight = 0f;
            }
            else if (pistol.activeSelf){
                isShootingAssault = false;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y - 15f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isAssaultShooting", false);
                selfAnim.SetBool("isAssaultAiming", false);

                aimRigPistol.weight = 1f;
                aimRigAssault.weight = 0f;
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
                isShootingPistol = false;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y + 52f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isPistolShooting", false);
                selfAnim.SetBool("isAiming", false);

                aimRigPistol.weight = 0f;
                aimRigAssault.weight = 1f;
                if (starterAssetsInputs.attack) {
                    selfAnim.SetBool("isAssaultShooting", true);
                    selfAnim.SetBool("isAssaultAiming", false);

                    if(isShootingAssault == false){
                        isShootingAssault = true;
                        StartCoroutine(shootingAssault());
                    } 
                }
                else {
                    selfAnim.SetBool("isAssaultShooting", false);
                    selfAnim.SetBool("isAssaultAiming", true);
                }
            }
        }
        else {
            if(starterAssetsInputs.move.x != 0 || starterAssetsInputs.move.y != 0 || starterAssetsInputs.jump) {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            aimRigPistol.weight = 0f;
            aimRigAssault.weight = 0f;
            selfAnim.SetBool("isAttacking", false);
            selfAnim.SetBool("isPistolShooting", false);
            selfAnim.SetBool("isAiming", false);
            selfAnim.SetBool("isAssaultShooting", false);
            selfAnim.SetBool("isAssaultAiming", false);
        }
    }

    IEnumerator shootingPistol(){
        isShootingPistol = true;
        yield return new WaitForSeconds(0.6f);
        if (starterAssetsInputs.attack && pistol.activeSelf) {
            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointPistol.position).normalized;
            Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection, Vector3.up));
        }
        isShootingPistol = false;
    }

    IEnumerator shootingAssault(){
        isShootingAssault = true;
        yield return new WaitForSeconds(0.2f);
        if (starterAssetsInputs.attack && assault.activeSelf) {
            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointAssault.position).normalized;
            Instantiate(bullet, bulletSpawnPointAssault.position, Quaternion.LookRotation(shootDirection, Vector3.up));
        }
        isShootingAssault = false;
    }
}
