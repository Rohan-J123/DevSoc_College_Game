using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    private Animator selfAnim;

    [SerializeField] private GameObject grenade;
    [SerializeField] private Transform grenadeStart;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject grenadePrefab;

    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        selfAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.grenade)
        {
            selfAnim.SetBool("isThrowing", true);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
            player = GameObject.FindGameObjectWithTag("Player");
            grenadePrefab.SetActive(true);
        }
        else
        {
            grenadePrefab.SetActive(false);
            selfAnim.SetBool("isThrowing", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }

    public void grenadeThrow()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        grenadePrefab.SetActive(false);
        var g = GameObject.Instantiate(grenade, grenadeStart.position, player.transform.rotation);
        g.GetComponent<Rigidbody>().AddForce(15f * cameraForward, ForceMode.Impulse);

    }
}
