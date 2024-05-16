using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    [SerializeField] private int livesRemaining;


    private void Start()
    {
        livesRemaining = lives.Length;
    }

    public void LoseLife()
    {
        if (livesRemaining == 0)
            return;

        livesRemaining--;

        if (livesRemaining >= 0 && livesRemaining < lives.Length)
        {
            lives[livesRemaining].enabled = false;
        }

        // probably don't need this
        if (livesRemaining == 0)
        {
            Debug.Log("You Lost");
        }
    }

    public int LivesRemaining()
    {
        return livesRemaining;
    }
}
