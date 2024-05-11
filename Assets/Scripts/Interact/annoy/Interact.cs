/*using System.Collections;
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
                /* Instantiate(text, canvas.transform);
                 gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);
                }
                }
                }
                }*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
    {
        [SerializeField] private bool inReach;
        [SerializeField] GameObject text;
        [SerializeField] GameObject canvas;

        // Event delegate for interaction event
        public delegate void InteractionEventHandler(Transform target);
        public static event InteractionEventHandler OnInteract;

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
                    // Instantiate text or perform other interaction UI actions
                    Instantiate(text, canvas.transform);

                    gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);

                // Find the player object and obtain its transform
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Transform playerTransform = player.transform;

                // Notify AI that interaction occurred with the player's transform
                OnInteract?.Invoke(playerTransform);
            }
            }
        }
    }