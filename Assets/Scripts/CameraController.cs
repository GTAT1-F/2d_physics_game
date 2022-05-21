using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Change the destination of the camera
    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        
    }
}
