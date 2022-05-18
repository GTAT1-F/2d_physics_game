using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private SpriteRenderer spriteRenderer;
    private float dirX;
    private Animator anim;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private UInt32 livePoint = 1;
    [SerializeField] private Text livePoints;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (0 < dirX)
        {
            anim.SetBool("running", true);
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0)
        {
            anim.SetBool("running", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cherry"))
        {
            ++livePoint;
            Destroy(col.gameObject);
            livePoints.text = "LivePoint: " + livePoints;
        }
    }
}
