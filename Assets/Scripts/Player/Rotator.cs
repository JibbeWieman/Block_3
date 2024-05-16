using UnityEngine;

/// <summary>
/// Rotates a gameObject around the specified axis at the specified speed
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45;
    [SerializeField] private Vector3 rotationAxis;

    /// <summary>
    /// Do the rotation
    /// </summary>
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }
}