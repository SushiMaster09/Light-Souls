using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Player Melee Combat Stats")]
    [SerializeField] private int damageAmount = 20;

    private PlayerController playerController;
    private Rigidbody2D rb;
    //private MeleeAttackManager meleeAttackManager;

    private Vector2 direction;
    private bool collided;
    private bool downwardStrike;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
        //meleeAttackManager = GetComponentInParent<MeleeAttackManager>();
    }

    private void FixedUpdate()
    {
        //HandleMovement()
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
