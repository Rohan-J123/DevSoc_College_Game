using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    private Animator selfAnim;
    private Transform selfTransform;
    private StarterAssetsInputs starterAssetsInputs;
    private PlayerAnimationController playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        selfAnim = GetComponent<Animator>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        selfTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.move.y > 0 && !selfAnim.GetBool("hasReachedTop"))
        {
            selfAnim.SetBool("isClimbingUp", true);
            selfAnim.SetBool("isClimbingDown", false);
            selfTransform.position += new Vector3(0f, 0.02f, 0f);
        }

        if (starterAssetsInputs.move.y < 0)
        {
            selfAnim.SetBool("isClimbingUp", false);
            selfAnim.SetBool("isClimbingDown", true);
            selfTransform.position += new Vector3(0f, -0.02f, 0f);
        }

        if(starterAssetsInputs.move.y == 0)
        {
            selfAnim.SetBool("isClimbingUp", false);
            selfAnim.SetBool("isClimbingDown", false);
        }

        if (starterAssetsInputs.jump)
        {
            playerAnimationController.isClimbing = false;
            selfTransform.position -= new Vector3(0f, 0f, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "LadderTop")
        {
            selfAnim.SetBool("hasReachedTop", true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "LadderTop")
        {
            selfAnim.SetBool("hasReachedTop", false);
        }
    }

    public void setIsClimbingFalse()
    {
        playerAnimationController.isClimbing = false;
        selfTransform.position += new Vector3(0f, 1f, 1f);
    }
}
