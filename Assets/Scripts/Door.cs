using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAnimName, doorCloseAnimName;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            //If the object the raycast hits is tagged as door
            if (hit.collider.gameObject.tag == "door")
            {
                //A GameObject variable is created for the door's main parent object
                GameObject doorParent = hit.collider.transform.root.gameObject;

                //An Animator variable is created for the doorParent's Animator component
                Animator doorAnim = doorParent.GetComponent<Animator>();

                //An AudioSource variable is created for the door's Audio Source component
                AudioSource doorSound = hit.collider.gameObject.GetComponent<AudioSource>();

                //The interaction text is set active
                intText.SetActive(true);

                //If the E key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //If the door's Animator's state is set to the open animation
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    {

                        //The door close sound will play
                        doorSound.Play();

                        //The door's open animation trigger is reset
                        doorAnim.ResetTrigger("open");

                        //The door's close animation trigger is set (it plays)
                        doorAnim.SetTrigger("close");
                    }
                    //If the door's Animator's state is set to the close animation
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    {

                        //The door open sound will play
                        doorSound.Play();

                        //The door's close animation trigger is reset
                        doorAnim.ResetTrigger("close");

                        //The door's open animation trigger is set (it plays)
                        doorAnim.SetTrigger("open");
                    }
                }
            }
            //else, if not looking at the door
            else
            {
                //The interaction text is disabled
                intText.SetActive(false);
            }
        }
        //else, if not looking at anything
        else
        {
            //The interaction text is disabled
            intText.SetActive(false);
        }
    }

}
