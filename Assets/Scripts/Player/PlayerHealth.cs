using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxStrikes = 3;
    public int currentStrikes;
    public static bool isDead;

    public GameObject gameOverPanel;
    public GameObject player;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        isDead = false;
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
            isDead = true;
        }

        
        if (lifeCountScript != null)
        {
            lifeCountScript.LoseLife();
        }
    }
}
