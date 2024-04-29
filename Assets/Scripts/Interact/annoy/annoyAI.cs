using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class annoyAI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 180; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.009f);
        }

        if(transform.position.y > 300)
        {
            Destroy(gameObject);
        }
    }
}
