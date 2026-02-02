using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Player Melee Combat Stats")]
    [SerializeField] private int damageAmount = 20;

    private PlayerController playerController;
    private Rigidbody2D rb;
    private MeleeAttackManager meleeAttackManager;

    private Vector2 direction;
    private bool collided;
    private bool downwardStrike;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
        meleeAttackManager = GetComponentInParent<MeleeAttackManager>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>())
        {
            HandleCollision(collision.GetComponent<EnemyHealth>());
        }
    }

    private void HandleCollision(EnemyHealth objHealth)
    {
        if (objHealth.giveUpwardForce && Input.GetAxis("Vertical") < 0 && !playerController.IsGrounded())
        {
            direction = Vector2.up;
            downwardStrike = true;
            collided = true;
        }
        if (Input.GetAxis("Vertical") > 0 && !playerController.IsGrounded())
        {
            direction = Vector2.down;
            downwardStrike = true;
            collided = true;
        }
        if (Input.GetAxis("Vertical") <= 0 && playerController.IsGrounded() || Input.GetAxis("Vetical") == 0)
        {
            if (playerController.isFacingRight)
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }

            collided = true;
        }

        objHealth.Damage(damageAmount);
        StartCoroutine(NoLongerColliding());
    }

    private void HandleMovement()
    {
        if (collided)
        {
            if (downwardStrike)
            {
                rb.AddForce(direction * meleeAttackManager.upwardsForce);
            }
            else
            {
                rb.AddForce(direction * meleeAttackManager.defaultForce);
            }
        }
    }

    private IEnumerator NoLongerColliding()
    {
        yield return new WaitForSeconds(meleeAttackManager.movementTime);
        collided = false;
        downwardStrike = false;
    }
}
