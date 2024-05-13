using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnnoyCounter : MonoBehaviour
{
    int countLocal = 0;
    public static bool CountSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Interact.OnInteractCount += HandleInteractCountEvent;
        Debug.Log(countLocal);
    }

    void OnGUI()
    {
        if(countLocal != 0)
        {
            var countLocalString = (countLocal+1).ToString();
            GUI.Label(new Rect(50, 70, 400, 200), countLocalString + "/4 Annoyances");
        }
        else
        {
            GUI.Label(new Rect(50, 70, 400, 200), "0/4 Annoyances");
        }

    }

    private void HandleInteractCountEvent(int CountTotal)
    {

        if (CountSwitch == true)
        {
            CountSwitch = false;
            countLocal ++;
            Debug.Log(CountTotal + " ur mom");
        }
    }
}
