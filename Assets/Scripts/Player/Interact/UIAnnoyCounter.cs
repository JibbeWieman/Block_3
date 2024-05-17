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
        headStyle.richText = true;

        GUI.Label(new Rect(50, 90, 400, 200), "Explore the House to Annoy the Landlord and Gather Evidence", headStyle);

        if (countAnoyLocal != 0)
        {
            var countLocalString = (countAnoyLocal).ToString();
            GUI.Label(new Rect(50, 120, 400, 200), "<Color=yellow>" + countLocalString + "/4 Annoyances</color>", headStyle);
        }
        else
        {
            GUI.Label(new Rect(50, 120, 400, 200), "<Color=yellow>0/4 Annoyances</color>", headStyle);
        }

        if (countCameraLocal != 0)
        {
            var countLocalString = (countCameraLocal).ToString();
            GUI.Label(new Rect(50, 150, 400, 200), "<Color=blue>" + countLocalString + "/4 Evidence</color>", headStyle);
        }
        else
        {
            GUI.Label(new Rect(50, 150, 400, 200), "<Color=blue>0/4 Evidence</color>", headStyle);
        }

        if(countAnoyLocal + countCameraLocal >= 8)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 400, 200), "YOU WIN", headStyle);
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
