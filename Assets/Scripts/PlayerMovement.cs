using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;
    private SpriteRenderer spriteRenderer;
    private float dirX;
    private Animator anim;
    private bool isFacingRight;
    private enum MovementState
    {
        idle, running, jumping, falling
    }

    // the parameters/constraints for player movements
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        isFacingRight = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed * (40*Time.deltaTime), player.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    //To keep the update method clean, created a seperate method that gets called in update method 
    private void UpdateAnimationState()
    {
        // enum is used here, so state is used to indicate which animation should be running (jumping, running, etc)
        MovementState state;
        
        // to rotate player if they face left/right
        // using rotate instead of flip so that the FirePoint where the bullets spawn also rotates with the player
        if (0 < dirX)
        {
            state = MovementState.running;
            if (isFacingRight == false)
            {
                isFacingRight = true;
                spriteRenderer.transform.Rotate(0f, 180, 0f);
            }
        }
        else if (dirX < 0) 
        {
            if (isFacingRight == true)
            {
                isFacingRight = false;
                spriteRenderer.transform.Rotate(0f, 180, 0f);
            }
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle; 
        }

        if (.1f < player.velocity.y)
        {
            state = MovementState.jumping;
        }
        
        else if (player.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state" , (int)(state));
    }

    // to prevent the player from jumping on air
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
