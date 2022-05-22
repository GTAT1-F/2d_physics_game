using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys the spiked ball after a certain time period or when it collides with other objects.
/// </summary>
public class SpikedBall : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float lifetime;

    private void Start()
    {
        time = 0f;
        lifetime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var spikedBall = collision.gameObject.GetComponent<SpikedBall>();
        // Destroy the spiked ball if it collides with anything other than another spiked ball
        if(spikedBall == null)
        {
            Destroy(gameObject);
        }
    }
}
