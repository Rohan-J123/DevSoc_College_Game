using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private RuntimeAnimatorController climb;
    [SerializeField] private RuntimeAnimatorController movement;
    public bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimbing)
        {
            animator.runtimeAnimatorController = climb;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerAim>().enabled = false;
            GetComponent<PlayerThrow>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<Climbing>().enabled = true;
        }
        else
        {
            animator.runtimeAnimatorController = movement;
            GetComponent<PlayerAttack>().enabled = true;
            GetComponent<PlayerAim>().enabled = true;
            GetComponent<PlayerThrow>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<Climbing>().enabled = false;
        }
    }
}
