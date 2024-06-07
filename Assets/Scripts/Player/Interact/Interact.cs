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
    [SerializeField] Animator Animator;

    Animator _animator;


    // Event delegate for interaction event
    public delegate void InteractionEventHandler(Transform target);
    public static event InteractionEventHandler OnInteract;

    public delegate void InteractAnoyEventHandler(int Count);
    public static event InteractAnoyEventHandler OnInteractCountAnoy;

    void Start()
    {
        inReach = false;
        targetTimePause += targetTimePauseStart;
        if (Sparkles != null)
        {
            Sparkles.SetActive(true);
        }
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
            GUI.Label(new Rect(50, 50, 400, 200), "Landlord Frozen for: " + targetTimePauseString, headStyle);
        }

        if (inReach == true && AmountCanInteract > 0)
        {
            if (CameraObject == true)
            {
                GUI.Box(new Rect(550, 550, Screen.width / 3, Screen.height / 2), "Press | P | to take Photo", headStyle);
            }
            else
            {
                GUI.Box(new Rect(550, 550, Screen.width / 3, Screen.height / 2), "Press | E | to interact", headStyle);
            }
        }

        if (targetTimePause > 7 && targetTimePause < 10)
        {
            GUI.Label(new Rect(550, 600, 400, 200), objectString, headStyle);
        }
    }

    void Update()
    {
        // Find the player object and obtain its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;

        if (CameraObject == false)
        {
            if (inReach && Input.GetButtonDown("Interact") && AmountCanInteract > 0 && PauseTimerOn == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 300, 1);

                    // Notify AI that interaction occurred with the player's transform
                    OnInteract?.Invoke(playerTransform);
                    UIAnnoyCounter.CountSwitch = true;

                    //Sets timer amount and starts it(currently 10 seconds)
                    PauseTimerOn = true;

                    //Can interact with it one less time
                    --AmountCanInteract;

                    Animator.SetInteger("State", 2);

                    if (Sparkles != null)
                    {
                        Sparkles.SetActive(false);
                    }

                    OnInteractCountAnoy?.Invoke(1);
                }
            }
        }
        else
        {

        }


        if (PauseTimerOn == true)
        {
            if (CameraObject == false)
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
                    GuardBT.speed = 5f;
                    Animator.SetInteger("State", 0);
                }
                //else the landlord stays in place(for as long as the timer is active(up to 10s))
                else
                {
                    OnInteract?.Invoke(LandlordTransform);
                    GuardBT.speed = 0f;
                    Animator.SetInteger("State", 2);
                }

                if (targetTimePause <= 0.0f)
                {
                    GameObject block = GameObject.FindGameObjectWithTag("block");
                    Transform blockTransform = block.transform;
                    OnInteract?.Invoke(blockTransform);
                    GuardBT.speed = 5f;
                    Animator.SetInteger("State", 0);
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
