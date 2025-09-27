using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed; // Speed of the camera smoothing
    void Update()
    {
        //linear interpolate towards player
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition; 

    }
}
