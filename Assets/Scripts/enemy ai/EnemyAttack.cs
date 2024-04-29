using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    //public float damageAmount = 10f;
    /*public float attackRange = 1.5f;

    private void Update()
    {
        // Perform raycast to check for the player
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeStrike();
                }
            }
        }
    }*/
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeStrike(); 
                }
                lastAttackTime = Time.time; 
            }
        }
    }
}
