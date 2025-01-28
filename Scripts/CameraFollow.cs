using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player to follow
    public float smoothSpeed = 0.125f; // Camera Smoothness
    public Vector3 offset; // Offset from the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position with offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
