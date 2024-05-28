using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public GameObject InventoryCanvas;
    static public bool showInventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;

        }

        if (showInventory)
        {
            InventoryCanvas.SetActive(true);
        } else
        {
            InventoryCanvas.SetActive(false);
        }
    }
}