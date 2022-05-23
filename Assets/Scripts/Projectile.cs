using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        lifetime = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if(startTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
