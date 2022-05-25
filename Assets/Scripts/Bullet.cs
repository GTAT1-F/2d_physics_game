using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed=20;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int damage =10;
    // Start is called before the first frame update
    private void Start()
    {
        rb.velocity = transform.right * speed * (40*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Boss boss = col.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
        Debug.Log(col.name);
        Instantiate(impactEffect, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }
}
