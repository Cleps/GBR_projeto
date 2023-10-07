using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f; 
    public Vector2 offset = new Vector2(0f, 0f);

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
