using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    [SerializeField] private bool inReach;

    [SerializeField] GameObject text;
    [SerializeField] GameObject canvas;

    void Start()
    {
        inReach = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
        }
    }


    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                /*Ray ray = new Ray(transform.position, transform.forward);
                 if(Physics.Raycast(ray, out RaycastHit hitInfo, number))
                 {*/
                Instantiate(text, canvas.transform);
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);
            }
        }
    }
}
