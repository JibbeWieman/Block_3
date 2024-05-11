/*using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 5f;
    public static float fovRange = 5f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });
   
        //Node root = new TaskPatrol(transform, waypoints);

        return root;
    }
}*/

/*using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GuardBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 5f;
    public static float fovRange = 5f;
    public static float attackRange = 1f;

    // Reference to the player transform
    private Transform playerTransform;

    protected override Node SetupTree()
    {
        // Subscribe to the interaction event
        Interact.OnInteract += HandleInteractEvent;

        Node root = new Selector(new List<Node>
        {
            // Approach the player if interaction occurred
            new Sequence(new List<Node>
            {
                new Conditional(() => playerTransform != null), // Check if player transform is not null
                new TaskGoToTarget(transform),
            }),
            // Continue patrolling if no interaction occurred
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }

    // Event handler for interaction event
    private void HandleInteractEvent(Transform target)
    {
        // Set the player transform reference
        playerTransform = target;
        Debug.Log("Player interaction detected. Approaching player.");
    }

    // Unsubscribe from the interaction event when the object is destroyed
    private void OnDestroy()
    {
        Interact.OnInteract -= HandleInteractEvent;
    }
}*/

using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GuardBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 5f;
    public static float fovRange = 5f;
    public static float attackRange = 1f;

    // Reference to the player transform
    private Transform playerTransform;

    private bool playerInteracted = false;

    protected override Node SetupTree()
    {
        // Subscribe to the interaction event
        Interact.OnInteract += HandleInteractEvent;

        Node root = new Selector(new List<Node>
        {
            // Follow the player if they have interacted with objects
            new Sequence(new List<Node>
            {
                new Conditional(() => playerInteracted), // Check if player interacted
                new TaskGoToTarget(transform),
            }),
            // Continue patrolling if no interaction occurred
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }

    // Event handler for interaction event
    private void HandleInteractEvent(Transform target)
    {
        // Set the player transform reference
        playerTransform = target;
        Debug.Log("Player interaction detected. Approaching player.");

        // Set the flag to indicate player interaction
        playerInteracted = true;

        // Debug log to check if playerInteracted flag is being set
        Debug.Log("Player interacted: " + playerInteracted);

        // Pass the player transform to TaskGoToTarget node
        _root.SetData("target", playerTransform);
    }

    // Unsubscribe from the interaction event when the object is destroyed
    private void OnDestroy()
    {
        Interact.OnInteract -= HandleInteractEvent;
    }
}

