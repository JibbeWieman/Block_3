using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime < 1020)
        {
            remainingTime += Time.deltaTime;
        }
        else if (remainingTime > 1020)
        {
            remainingTime = 1020;
            gameOverPanel.SetActive(true);
        }
     
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
