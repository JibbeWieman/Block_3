using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    private PlayerHealth playerHealth;

    bool m_IsPlayerInRange;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    //playerHealth.TakeDamage(30);
                }
            }
        }
    }
}
