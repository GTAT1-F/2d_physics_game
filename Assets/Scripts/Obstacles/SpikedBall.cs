using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys the spiked ball after a certain time period or when it collides with something.
/// </summary>
public class SpikedBall : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject collisionPrefab;

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
        // Play collision animation, then destroy spiked ball
        Instantiate(collisionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
