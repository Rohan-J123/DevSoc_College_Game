using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UI;

public class DeadNPCScript : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private int weaponBulletType;
    [SerializeField] private int numberOfBullets;
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text collectText;

    // Start is called before the first frame update
    void Start()
    {
       weaponBulletType = Random.Range(1, 3);
       numberOfBullets = Random.Range(10, 20);
       player = GameObject.FindGameObjectWithTag("Player");
       collectText = GameObject.FindGameObjectWithTag("CollectText").GetComponent<TMP_Text>();
       starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
       collectText.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position - transform.position).magnitude < 3f){
            if(weaponBulletType == 1){
                collectText.SetText(numberOfBullets + " PISTOL BULLETS [PRESS C]");
                if(starterAssetsInputs.collect){
                    player.GetComponent<PlayerAttack>().bulletCountPistol += numberOfBullets;
                    collectText.SetText("");
                    Destroy(this);
                }
            }
            else if(weaponBulletType == 2){
                collectText.SetText(numberOfBullets * 2 + " ASSAULT BULLETS [PRESS C]");
                if(starterAssetsInputs.collect){
                    player.GetComponent<PlayerAttack>().bulletCountAssault += (numberOfBullets * 2);
                    collectText.SetText("");
                    Destroy(this);
                }
            }
            
        }
        else{
            collectText.SetText("");
        }
    }
}
