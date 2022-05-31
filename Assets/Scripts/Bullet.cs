using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed = 50;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] public int damage { get; private set; } = 10;
    // a prefab that speeds up the player's speed
    [SerializeField] private GameObject speedUpPrefab;
    // prefab that reduces a player's life
    [SerializeField] private GameObject lifeTakePrefab;
    private void Start()
    {
        rb.velocity = transform.right * speed * (40 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // after a player breaks the box, a random effect is generated
        if (col.gameObject.CompareTag("Box"))
        {
            Destroy(col.gameObject);
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
                Instantiate(speedUpPrefab, col.bounds.center, Quaternion.identity);
            }
            else if (random == 1)
            {
                Instantiate(lifeTakePrefab, col.bounds.center, Quaternion.identity);
            }
        }
        Boss boss = col.GetComponent<Boss>();
        if (boss != null)
        {
            //boss.TakeDamage(damage);
        }
        Debug.Log(col.name);
        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
