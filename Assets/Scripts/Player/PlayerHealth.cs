using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /*public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Debug.Log("Player died!");
        
    }*/

  
    public int maxStrikes = 3;
    public int currentStrikes;

    public GameObject gameOverPanel;
    public GameObject player;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        currentStrikes = 0;
    }

    public void TakeStrike()
    {
        currentStrikes++;

        LifeCount lifeCountScript = FindObjectOfType<LifeCount>();

        if (currentStrikes >= maxStrikes || (lifeCountScript != null && lifeCountScript.LivesRemaining() == 0))
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameOverPanel.SetActive(true);
        }

        
        if (lifeCountScript != null)
        {
            lifeCountScript.LoseLife();
        }
    }
}
