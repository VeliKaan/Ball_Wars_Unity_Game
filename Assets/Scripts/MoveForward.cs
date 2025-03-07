using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed = 5.0f; // Movement speed of the object

    [HideInInspector]
    public Vector3 LookDirection; // Direction in which the object is moving

    private void Start()
    {
        // Save the initial position
        Vector3 startPosition = transform.position;

        // Move the object slightly upwards to determine the movement direction
        transform.Translate(Vector3.up);

        // Calculate the movement direction based on the displacement
        LookDirection = transform.position - startPosition;

        // Reset the position back to the original start position
        transform.position = startPosition;
    }

    void Update()
    {
        // Move the object upward continuously at the specified speed
        transform.Translate(new Vector3(0, Time.deltaTime * Speed, 0));
    }
}
