using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    private Animator selfAnim;
    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject assault;

    [SerializeField] private GameObject bullet;

    //private bool isShootingPistol = false;
    //private bool isShootingAssault = false;

    [SerializeField] private Transform bulletSpawnPointPistol;
    [SerializeField] private Transform bulletSpawnPointAssault;

    [SerializeField] private Rig aimRigPistol;
    [SerializeField] private Rig aimRigAssault;

    public Vector3 mouseWorldPosition;
    public bool audioEnabled;

    [SerializeField] private TMP_Text knifeText;
    [SerializeField] private TMP_Text pistolText;
    [SerializeField] private TMP_Text assaultText;

    public int bulletCountPistol;
    public int bulletCountAssault;

    private AudioSource assaultAudioSource;
    [SerializeField] private AudioClip assaultAudioClip;

    private AudioSource pistoltAudioSource;
    [SerializeField] private AudioClip pistolAudioClip;

    private PlayerAnimationController playerAnimationController;

    [SerializeField] private GameObject crossHairImage;

    // Start is called before the first frame update
    void Start()
    {
        selfAnim = GetComponent<Animator>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        bulletCountAssault = 100;
        bulletCountPistol = 30;

        knifeText.fontSize = 16;
        pistolText.fontSize = 12;
        assaultText.fontSize = 12;

        assaultAudioSource = assault.GetComponent<AudioSource>();
        pistoltAudioSource = pistol.GetComponent<AudioSource>();

        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        pistolText.SetText(""+bulletCountPistol);
        assaultText.SetText("" + bulletCountAssault);
        
        if (starterAssetsInputs.knife) {  
            knife.SetActive(true);
            pistol.SetActive(false);
            assault.SetActive(false);

            knifeText.fontSize = 16;
            pistolText.fontSize = 12;
            assaultText.fontSize = 12;
        }
        if (starterAssetsInputs.pistol) {
            knife.SetActive(false);
            pistol.SetActive(true);
            assault.SetActive(false);

            knifeText.fontSize = 12;
            pistolText.fontSize = 18;
            assaultText.fontSize = 12;
        }
        if (starterAssetsInputs.assault) {
            knife.SetActive(false);
            pistol.SetActive(false);
            assault.SetActive(true);

            knifeText.fontSize = 12;
            pistolText.fontSize = 12;
            assaultText.fontSize = 18;
        }

        if (starterAssetsInputs.aim)
        {
            crossHairImage.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            crossHairImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
                //isShootingAssault = false;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y - 15f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isAssaultShooting", false);
                selfAnim.SetBool("isAssaultAiming", false);

                aimRigPistol.weight = 1f;
                aimRigAssault.weight = 0f;
                if (starterAssetsInputs.attack && bulletCountPistol > 0) {
                    selfAnim.SetBool("isPistolShooting", true);
                    selfAnim.SetBool("isAiming", false);

                    //if(isShootingPistol == false){
                    //    isShootingPistol = true;
                    //    StartCoroutine(shootingPistol());
                    //}  
                }
                else {
                    selfAnim.SetBool("isPistolShooting", false);
                    selfAnim.SetBool("isAiming", true);
                }
            }
            else if (assault.activeSelf){
                //isShootingPistol = false;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y + 52f, transform.localEulerAngles.z);
                
                selfAnim.SetBool("isAttacking", false);
                selfAnim.SetBool("isPistolShooting", false);
                selfAnim.SetBool("isAiming", false);

                aimRigPistol.weight = 0f;
                aimRigAssault.weight = 1f;
                if (starterAssetsInputs.attack && bulletCountAssault > 0) {
                    selfAnim.SetBool("isAssaultShooting", true);
                    selfAnim.SetBool("isAssaultAiming", false);

                    //if(isShootingAssault == false){
                    //    isShootingAssault = true;
                    //    StartCoroutine(shootingAssault());
                    //} 
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

    //IEnumerator shootingPistol(){
    //    isShootingPistol = true;
    //    yield return new WaitForSeconds(0.6f);

    //    if (starterAssetsInputs.attack && pistol.activeSelf) {
    //        if (audioEnabled)
    //        {
    //            pistoltAudioSource.PlayOneShot(pistolAudioClip);
    //        }

    //        Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointPistol.position).normalized;
    //        float x = Random.Range(-0.03f, 0.03f);
    //        float y = Random.Range(-0.03f, 0.03f);
    //        float z = Random.Range(-0.03f, 0.03f);
    //        if (starterAssetsInputs.aim)
    //        {
    //            Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection + new Vector3(x / 2f, y / 2f, z / 2f), Vector3.up));
    //        }
    //        else
    //        {
    //            Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection + new Vector3(x, y, z), Vector3.up));
    //        }
    //        // Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection, Vector3.up));
    //        bulletCountPistol -= 1;
    //    }
    //    isShootingPistol = false;
    //}

    //IEnumerator shootingAssault(){
    //    isShootingAssault = true;
    //    if (audioEnabled)
    //    {
    //        assaultAudioSource.PlayOneShot(assaultAudioClip);
    //    }

    //    yield return new WaitForSeconds(0.2f);

    //    if (starterAssetsInputs.attack && assault.activeSelf) {
    //        Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointAssault.position).normalized;
    //        float x = Random.Range(-0.03f, 0.03f);
    //        float y = Random.Range(-0.03f, 0.03f);
    //        float z = Random.Range(-0.03f, 0.03f);
    //        if (starterAssetsInputs.aim){
    //            Instantiate(bullet, bulletSpawnPointAssault.position, Quaternion.LookRotation(shootDirection + new Vector3(x/2f, y/2f, z/2f), Vector3.up));
    //        }
    //        else
    //        {
    //            Instantiate(bullet, bulletSpawnPointAssault.position, Quaternion.LookRotation(shootDirection + new Vector3(x, y, z), Vector3.up));
    //        }
    //        bulletCountAssault -= 1;
    //    }
    //    isShootingAssault = false;
    //}

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Climbable")
        {
            playerAnimationController.isClimbing = true;
        }
    }

    public void pistolShooting()
    {
        if (starterAssetsInputs.attack && pistol.activeSelf && bulletCountPistol > 0)
        {
            if (audioEnabled)
            {
                pistoltAudioSource.PlayOneShot(pistolAudioClip);
            }

            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointPistol.position).normalized;
            float x = Random.Range(-0.03f, 0.03f);
            float y = Random.Range(-0.03f, 0.03f);
            float z = Random.Range(-0.03f, 0.03f);
            if (starterAssetsInputs.aim)
            {
                Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection + new Vector3(x / 2f, y / 2f, z / 2f), Vector3.up));
            }
            else
            {
                Instantiate(bullet, bulletSpawnPointPistol.position, Quaternion.LookRotation(shootDirection + new Vector3(x, y, z), Vector3.up));
            }
            bulletCountPistol -= 1;
        }
    }

    public void assaultShooting()
    {
        if (starterAssetsInputs.attack && assault.activeSelf && bulletCountAssault > 0)
        {
            if (audioEnabled)
            {
                assaultAudioSource.PlayOneShot(assaultAudioClip);
            }

            Vector3 shootDirection = (mouseWorldPosition - bulletSpawnPointAssault.position).normalized;
            float x = Random.Range(-0.03f, 0.03f);
            float y = Random.Range(-0.03f, 0.03f);
            float z = Random.Range(-0.03f, 0.03f);
            if (starterAssetsInputs.aim)
            {
                Instantiate(bullet, bulletSpawnPointAssault.position, Quaternion.LookRotation(shootDirection + new Vector3(x / 2f, y / 2f, z / 2f), Vector3.up));
            }
            else
            {
                Instantiate(bullet, bulletSpawnPointAssault.position, Quaternion.LookRotation(shootDirection + new Vector3(x, y, z), Vector3.up));
            }
            bulletCountAssault -= 1;
        }
    }
}
