using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnnoyCounter : MonoBehaviour
{
    int countAnoyLocal = 0;
    int countCameraLocal = 0;
    public static bool CountSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Interact.OnInteractCountAnoy += HandleInteractCountAnoyEvent;
        Interact.OnInteractCountCamera += HandleInteractCountCameraEvent;
    }

    void OnGUI()
    {
        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = 30;

        if (countAnoyLocal != 0)
        {
            var countLocalString = (countAnoyLocal).ToString();
            GUI.Label(new Rect(50, 80, 400, 200), countLocalString + "/4 Annoyances", headStyle);
        }
        else
        {
            GUI.Label(new Rect(50, 80, 400, 200), "0/4 Annoyances", headStyle);
        }

        if (countCameraLocal != 0)
        {
            var countLocalString = (countCameraLocal).ToString();
            GUI.Label(new Rect(50, 110, 400, 200), countLocalString + "/4 Evidence", headStyle);
        }
        else
        {
            GUI.Label(new Rect(50, 110, 400, 200), "0/4 Evidence", headStyle);
        }

    }

    private void HandleInteractCountAnoyEvent(int CountTotal)
    {
        if (CountSwitch == true)
        {
            CountSwitch = false;
            countAnoyLocal ++;
        }
    }

    private void HandleInteractCountCameraEvent(int CountTotal)
    {
        if (CountSwitch == true)
        {
            CountSwitch = false;
            countCameraLocal++;
        }
    }
}
