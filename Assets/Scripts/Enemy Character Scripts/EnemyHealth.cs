using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Stats")]
    [SerializeField] private bool damageable;
    [SerializeField] private int healthAmount = 100;
    [SerializeField] private float invulnerabilityTime = 0.2f;

    public bool giveUpwardForce = true;
    private bool hit;
    private int currentHealth;

    private void Start()
    {
        currentHealth = healthAmount;
    }

    public void Damage(int amount)
    {
        if (damageable && !hit && currentHealth > 0)
        {
            hit = true;
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(TurnOffHit());
            }
        }
    }

    private IEnumerator TurnOffHit()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        hit = false;
    }
}
