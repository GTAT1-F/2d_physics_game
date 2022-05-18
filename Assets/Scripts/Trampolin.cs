using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] private float trampolineForce;
    // Start is called before the first frame update
    void Start()
    {
        trampolineForce = 200f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        // Applies a force to the game object that collided with this if that game object has a rigidbody2d component attached
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.AddForce(new Vector2(0, trampolineForce), ForceMode2D.Force);
        }
    }
}
