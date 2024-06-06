using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError($"No animator found. Add animtor to object");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "stairs")
        {
            Debug.Log($"{gameObject.name} entered stairs");

            animator.SetInteger("State", 1); // climbing state
            //animator.SetBool("isWalking", false); // used in idle state to determine where to go back to
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stairs")
        {
            Debug.Log($"{gameObject.name} exited stairs");

            animator.SetInteger("State", 0); // walking state
            //animator.SetBool("isWalking", true); // used in idle state to determine where to go back to
        }
    }
}
