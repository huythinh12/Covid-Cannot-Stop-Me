using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 newPosition;
    private Vector3 newRotation;

    private void LateUpdate()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        
        newRotation = new Vector3(90f, player.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
