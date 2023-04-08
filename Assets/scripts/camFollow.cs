using System;
using UnityEngine;


public class camFollow : MonoBehaviour
{
    public Transform ball;
    public Vector3 distance;
    public float smoothValue;

    private void LateUpdate()
    {
        Vector3 distancePosition = ball.position + distance;
        Vector3 smoothedDistancePosition = Vector3.Lerp(transform.position, distancePosition, smoothValue);
        transform.position = smoothedDistancePosition;
        
        transform.LookAt(ball);
    }
}