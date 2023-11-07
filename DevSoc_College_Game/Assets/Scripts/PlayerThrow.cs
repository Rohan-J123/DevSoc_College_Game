using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    private Animator selfAnim;
    [SerializeField] private GameObject grenade;

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
            grenade.SetActive(true);
        }
        else
        {
            selfAnim.SetBool("isThrowing", false);
            grenade.SetActive(false);
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
}
