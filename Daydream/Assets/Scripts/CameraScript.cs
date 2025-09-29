using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject followTarget;
    [SerializeField] float followDistance;

    private float targetMovementSpeed;
    private float currentMovementSpeed;

    public float moveSpeed;
    public float accelleration;
    void Update()
    {
        DetectRelativePlayerDistance();
        Move();
    }

    private void Move()

    {
        currentMovementSpeed = Mathf.MoveTowards(currentMovementSpeed, targetMovementSpeed, accelleration * Time.deltaTime);

        Vector3 targetPos = followTarget.transform.position;
        targetPos.z = transform.position.z;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, currentMovementSpeed * Time.deltaTime);
    }

    private void DetectRelativePlayerDistance()
    {
        Vector3 totalDistance = followTarget.transform.position - transform.position;

        if(totalDistance.magnitude > followDistance)
            targetMovementSpeed = moveSpeed;
        else
            targetMovementSpeed = 0;
    }
}