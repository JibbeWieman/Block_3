using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAnnoy : MonoBehaviour, IInteractible
{
    [SerializeField] GameObject text;
    
    //this object has to be drag in from scene, not a prefab
    [SerializeField] GameObject canvas;


    public void Interact()
    {
        Instantiate(text, canvas.transform);
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);
    }
}
