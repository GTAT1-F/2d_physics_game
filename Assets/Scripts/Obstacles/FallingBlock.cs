using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    private Vector2 initialPosition;
    [SerializeField] private float fallSpeed;
    private float rayLength = 10f;
    private int playerLayer;
    [SerializeField] private bool movingUp;

    private void Start()
    {
        initialPosition = new Vector2(transform.position.x, transform.position.y);
        fallSpeed = 0.5f;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        movingUp = false;
    }
    private void FixedUpdate()
    {
        if (!movingUp)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, rayLength);
            Collider2D collider = hit.collider;

            if (hit.collider != null)
            {
                if (collider.gameObject.layer == playerLayer)
                {
                    ColliderDistance2D distance = hit.collider.Distance(boxCollider);
                    StartCoroutine(MoveDown());
                }
            }
            Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        }
    }


    private IEnumerator MoveDown()
    {
        while (true)
        {
            rigidBody.AddForce(Vector2.down * Time.fixedDeltaTime * fallSpeed, ForceMode2D.Impulse);
            yield return null;
        }
    }

    // Moves the block back up until it's in its starting position
    private IEnumerator MoveUp()
    {
        movingUp = true;
        while (gameObject.transform.position.y < initialPosition.y)
        {
            rigidBody.AddForce(Vector2.up * Time.fixedDeltaTime, ForceMode2D.Impulse);
            yield return null;
        }
        rigidBody.velocity = Vector3.zero;
        movingUp = false;
        StopCoroutine(MoveUp());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Move back up after colliding with something
        StopAllCoroutines();
        StartCoroutine(MoveUp());
    }

}