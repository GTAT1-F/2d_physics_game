using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockRigid : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    [SerializeField] private float fallSpeed;
    private float rayLength = 10f;
    private int layer;

    private void Start()
    {
        fallSpeed = 500f;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        layer = LayerMask.NameToLayer("Player");
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, rayLength);
        Collider2D collider = hit.collider;
        
        if (hit.collider != null)
        {
            if(collider.gameObject.layer == layer)
            {
                ColliderDistance2D distance = hit.collider.Distance(boxCollider);
                Debug.Log("distance " + distance.distance);
                Fall();
            }
        }
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
    }


    private void Fall()
    {
        rigidBody.AddForce(new Vector2(0, -1) * fallSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
    }
}

