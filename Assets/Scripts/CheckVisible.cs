using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVisible : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCheck;
    [SerializeField] private LayerMask layerToIgnore;

    Camera cam;
    Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public GameObject[] GetObjectsToCheck() => objectsToCheck;

    public bool ObjectInCameraFrustrum(GameObject objectToCheck)
    {
        bool isVisible = false;

        Vector3 viewPos = cam.WorldToViewportPoint(objectToCheck.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            // Your object is in the range of the camera, you can apply your behaviour
            isVisible = true;
        }

        return isVisible;
    }

    public bool ObjectBlocked(GameObject objectToCheck)
    {
        bool objectBlocked = true;

        RaycastHit hit;
        float distanceToObject = Vector3.Distance(cam.transform.position, objectToCheck.transform.position);
        direction = (objectToCheck.transform.position - cam.transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, 10))
            print("There is something in front of the object!");

        if (Physics.Raycast(cam.transform.position, direction, out hit, distanceToObject + 1))
        {
            if (hit.collider.gameObject.name == objectToCheck.name)
            {
                objectBlocked = false;
                // TODO
                // disable collider of boject to take picture from so you cannot take duplicate pictures
                BoxCollider collider = objectToCheck.GetComponent<BoxCollider>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }

        return objectBlocked;
    }
}