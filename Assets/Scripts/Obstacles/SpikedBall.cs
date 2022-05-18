using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to destroy the spiked ball after it leaves the camera's view and when it collides with another object
/// </summary>
public class SpikedBall : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if(viewPos.x < 0 || viewPos.x > 1)
        {
            GameObject.Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var spikedBall = collision.gameObject.GetComponent<SpikedBall>();
        // Destroy the spiked ball if it collides with anything other than another spiked ball
        if(spikedBall == null)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
