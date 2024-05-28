using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RCBattery : MonoBehaviour
{
    public Slider batterySlider;  // Reference to the UI Slider
    [SerializeField] float maxTime = 30f; // Maximum time in seconds
    [SerializeField] float remainingTime;

    void Start()
    {
        remainingTime = maxTime;

        // Initialize slider values
        batterySlider.maxValue = 1;
        batterySlider.minValue = 0;
        batterySlider.value = remainingTime / maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (RCmovement.rcActivated == true)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
            }

            // Update the slider value
            batterySlider.value = remainingTime / maxTime;
        }

        if(remainingTime == 0)
        {
            RCmovement.rcActivated = false;
        }
    }
}
