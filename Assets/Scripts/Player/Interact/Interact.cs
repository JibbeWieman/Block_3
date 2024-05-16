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

    [SerializeField] float targetTimePauseStart = 10.0f;
    float targetTimePause;
    [SerializeField] bool PauseTimerOn = false;
    [SerializeField] int AmountCanInteract = 1;
    [SerializeField] int AgroDistance;
    [SerializeField] bool CameraObject = false;
    [SerializeField] string objectString;
    [SerializeField] GameObject Sparkles;

    // Event delegate for interaction event
    public delegate void InteractionEventHandler(Transform target);
    public static event InteractionEventHandler OnInteract;

    public delegate void InteractAnoyEventHandler(int Count);
    public static event InteractAnoyEventHandler OnInteractCountAnoy;

    public delegate void InteractCameraEventHandler(int Count);
    public static event InteractCameraEventHandler OnInteractCountCamera;

    void Start()
    {
        inReach = false;
        targetTimePause += targetTimePauseStart;
        Sparkles.SetActive(true);
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

    void OnGUI()
    {
        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = 30;

        if (targetTimePause < targetTimePauseStart)
        {
            int targetTimePauseInt = (int)targetTimePause;
            var targetTimePauseString = targetTimePauseInt.ToString();
            GUI.Label(new Rect(50, 50, 400, 200), "Time Frozen " + targetTimePauseString, headStyle);
        }

        if (inReach == true && AmountCanInteract > 0)
        {
            GUI.Box(new Rect(550, 550, Screen.width/3, Screen.height/2), "Press | E | to interact", headStyle);
        }

        if(targetTimePause > 8 && targetTimePause < 10)
        {
            GUI.Label(new Rect(550, 600, 400, 200), objectString, headStyle);
        }
    }

    void Update()
    {
        // Find the player object and obtain its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;

        if (inReach && Input.GetButtonDown("Interact") && AmountCanInteract > 0 && PauseTimerOn == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);

                // Notify AI that interaction occurred with the player's transform
                OnInteract?.Invoke(playerTransform);
                UIAnnoyCounter.CountSwitch = true;

                if(CameraObject == false)
                {
                    OnInteractCountAnoy?.Invoke(1);
                }
                else
                {
                    OnInteractCountCamera?.Invoke(1);
                }

                //Sets timer amount and starts it(currently 10 seconds)
                PauseTimerOn = true;

                //Can interact with it one less time
                --AmountCanInteract;

                Sparkles.SetActive(false);
            }

        }

        if (PauseTimerOn == true)
        {
            if(CameraObject == false)
            {
                targetTimePause -= Time.deltaTime;

                GameObject Landlord = GameObject.FindGameObjectWithTag("Landlord");
                Transform LandlordTransform = Landlord.transform;

                //if there is less than 10 distance between landlord and tennant, they follow(for as long as timer is active)
                if (Mathf.Sqrt(Mathf.Pow((Landlord.transform.position.x - player.transform.position.x), 2) +
                              Mathf.Pow((Landlord.transform.position.y - player.transform.position.y), 2) +
                              Mathf.Pow((Landlord.transform.position.z - player.transform.position.z), 2)) < AgroDistance)
                {
                    OnInteract?.Invoke(playerTransform);
                }
                //else the landlord stays in place(for as long as the timer is active(up to 10s))
                else
                {
                    OnInteract?.Invoke(LandlordTransform);
                }

                if (targetTimePause <= 0.0f)
                {
                    GameObject block = GameObject.FindGameObjectWithTag("block");
                    Transform blockTransform = block.transform;
                    OnInteract?.Invoke(blockTransform);
                    targetTimePause = targetTimePauseStart;
                    PauseTimerOn = false;
                }
            }
            else
            {
                targetTimePause -= Time.deltaTime;

                OnInteract?.Invoke(playerTransform);

                if (targetTimePause <= 0.0f)
                {
                    GameObject block = GameObject.FindGameObjectWithTag("block");
                    Transform blockTransform = block.transform;
                    OnInteract?.Invoke(blockTransform);
                    targetTimePause = targetTimePauseStart;
                    PauseTimerOn = false;
                }
            }
            
        }
    }
}