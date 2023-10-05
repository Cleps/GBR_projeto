using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O transform do jogador que a câmera vai seguir
    public float smoothSpeed = 5f; // A velocidade suave de movimento da câmera
    public Vector2 offset = new Vector2(0f, 0f); // Offset para x e y

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // Usar SmoothDamp para suavizar o movimento da câmera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
