using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDestroy : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
