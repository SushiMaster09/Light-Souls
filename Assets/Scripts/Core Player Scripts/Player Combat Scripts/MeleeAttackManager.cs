using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackManager : MonoBehaviour
{
    [Header("Melee Attack Manager Stats")]
    public float defaultForce = 300f;
    public float upwardsForce = 600f;
    public float movementTime = 0.1f;

    private bool meleeAttack;

    private Animator meleeAnimator;
    private Animator anim;
    private PlayerController playerController;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.X)) { meleeAttack = true; }
        else { meleeAttack = false; }

        if (meleeAttack && Input.GetAxis("Vertical") > 0f)
        {
            anim.SetTrigger("UpwardMelee");
            meleeAnimator.SetTrigger("UpwardMeleeSwipe");
        }
        if (meleeAttack && Input.GetAxis("Vertical") < 0f && !playerController.IsGrounded())
        {
            anim.SetTrigger("DownwardMelee");
            meleeAnimator.SetTrigger("DownwardMeleeSwipe");
        }
        if ((meleeAttack && Input.GetAxis("Vertical") == 0) || meleeAttack && (Input.GetAxis("Vertical") < 0f && playerController.IsGrounded()))
        {
            anim.SetTrigger("ForwardMelee");

            meleeAnimator.SetTrigger("ForwardMeleeSwipe");
        }
    }
}
