using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Text;

public class NPCPatrolPoint : MonoBehaviour
{
    [SerializeField] protected float debugDrawRadius = 1.0F;
    [SerializeField] protected float _connectivityRadius = 50F;

    List<NPCPatrolPoint> _connections;

    public void Start()
    {
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("PatrolPoint");

        _connections = new List<NPCPatrolPoint>();

        for (int i = 0; i < allWaypoints.Length; i++)
        {
            NPCPatrolPoint nextWaypoint = allWaypoints[i].GetComponent<NPCPatrolPoint>();

            if (nextWaypoint != null)
            {
                Debug.Log(Vector3.Distance(this.transform.position, nextWaypoint.transform.position));
                if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                {
                    Debug.Log("Here");
                    _connections.Add(nextWaypoint);
                    Debug.Log(_connections.ToString());
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connectivityRadius);

    }

    public NPCPatrolPoint NextWaypoint(NPCPatrolPoint previousWaypoint)
    {
        if (_connections.Count == 0)
        {
            Debug.LogError("Insufficient waypoint count");
            return null;
        }
        else if (_connections.Count == 1 && _connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            NPCPatrolPoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                nextWaypoint = _connections[nextIndex];
            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;

        }

    }
}