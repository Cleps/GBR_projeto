using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private float parallaxSpeed = 1.0f; 
    [SerializeField]
    private GameObject obj;
    public Transform targetbg; 
    public float smoothSpeedbg = 5f; 
    public Vector2 offsetbg = new Vector2(0f, 0f); 

    private void Update()
    {
        // Mover p a esquerda
        obj.transform.Translate(Vector3.left * parallaxSpeed * Time.deltaTime);

        // Resetar a posição
        if (obj.transform.position.x <= -40.0f)
        {
            // Reposicionar
            obj.transform.position = new Vector2(0f, transform.position.y);
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(obj.transform.position.x + offsetbg.x, targetbg.position.y + offsetbg.y, obj.transform.position.z);
        obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, smoothSpeedbg * Time.deltaTime);
    }
}
