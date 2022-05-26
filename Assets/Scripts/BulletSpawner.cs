using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private const int AMOUNT_ATTACK_SOUNDS = 4;
    [SerializeField] private AudioSource[] attackSound = new AudioSource[AMOUNT_ATTACK_SOUNDS];
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

            int randomIndex = Random.Range(0, 5);
            attackSound[randomIndex].Play();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
