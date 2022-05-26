using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using bsm = BossStateMachine;
using System;


public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject spikedBallPrefab;
    private Transform bossTransform => transform;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform[] leftAttackPoints;
    [SerializeField] private Transform[] rightAttackPoints;

    [SerializeField] public Sprite[] landingAnimationSprites;
    [SerializeField] private int spriteIndex = 0;
    public int Health { get => health ; private set => health = value; }
    [SerializeField] private int health;

    [SerializeField] public float ballForce;
    private bool isGrounded;

    [SerializeField] TextMeshPro damageText;
    [SerializeField] bsm.BossStateMachine stateMachine;

    public List<string> possibleActions;
    public string currentAction;
    public int actionsCount;

    public float startingAttackDuration;
    public float attackDuration;
    [SerializeField] private bool attackInProgress;
    [SerializeField] private bool playerInBox;
    
    private int groundLayer; 
    private int groundLayerMask;
    private int playerAttackLayer;
    private int playerLayerMask;

    bool animating = false;

    // Start is called before the first frame update
    void Start()
    {
        attackDuration = 5f;
        health = 50;
        ballForce = 100f;
        startingAttackDuration = 8f;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine = GetComponentInChildren<bsm.BossStateMachine>();
        possibleActions = new List<string>();
        currentAction = null;
        groundLayer = LayerMask.NameToLayer("Ground");
        groundLayerMask = LayerMask.GetMask(new string[] {"Ground"});
        playerAttackLayer = LayerMask.NameToLayer("PlayerAttacks");
        playerLayerMask = LayerMask.GetMask(new string[] {"Player"});
    }

    // Update is called once per frame
    void Update()
    {
        // Check if boss is on the ground
        isGrounded = boxCollider.IsTouchingLayers(groundLayerMask);
        // Check if player is nearby
        playerInBox = CheckForPlayer();

        if (!playerInBox)
        {
            stateMachine.Trigger(bsm.BossTransitions.Idle);
        }
        else
        {
            if (health > 25)
            {
                stateMachine.Trigger(bsm.BossTransitions.AttackPhase1);
            }
            else if (health > 0)
            {
                stateMachine.Trigger(bsm.BossTransitions.AttackPhase2);
            }
            else
            {
                stateMachine.Trigger(bsm.BossTransitions.Death);
            }
        }

        // Update the timer
        if (!attackInProgress)
        {
            attackDuration = startingAttackDuration;
        }
        else
        {
            attackDuration -= Time.deltaTime;
        }

        // Choose an attack of the attacks possible in boss's current state after the previous attack has played for attackDuration
        if (!attackInProgress)
        {
            if (currentAction != null)
            {
                Debug.LogWarning("stopping all coroutines");
                StopAllCoroutines();
            }
            if (actionsCount > 0)
            {
                currentAction = possibleActions[UnityEngine.Random.Range(0, actionsCount)];
                StartCoroutine(currentAction);
            }
        }

        // Check if attack is finished
        attackInProgress = attackDuration > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        // Take damage if collision was with player bullet
        if (go.layer == playerAttackLayer)
        {
            StartCoroutine(TakeDamage(1)); // TakeDamage(player.getDamage())
        }
        // Play landing animation if collision was with the ground
        if (go.layer == groundLayer)
        {
            //StartCoroutine(LandingAnimation());

        }
    }
    // Instantiates a spiked ball at the center attack point on each side
    public IEnumerator BallAttack()
    {
        Transform leftAttackPoint = leftAttackPoints[1];
        var ball = Instantiate(spikedBallPrefab, leftAttackPoint);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));

        Transform rightAttackPoint = rightAttackPoints[1];
        ball = Instantiate(spikedBallPrefab, rightAttackPoint);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));

        yield return null;
    }

    public IEnumerator TakeDamage(int damage)
    {
        Health -= damage;

        damageText.color = Color.red;
        damageText.text = damage.ToString();

        yield return new WaitForSeconds(0.5f);

        damageText.color = Color.green;
        damageText.text = health.ToString();
    }

    // Boss moves up and does a ball attack when it lands
    public IEnumerator UpDownAttack()
    {
        while (true)
        {
            yield return StartCoroutine(MoveUp(10f));
            yield return StartCoroutine(WaitToLand());
            yield return StartCoroutine(BallAttack());
            yield return null;
        }  
    }

    // Sends out balls from the bottom, then center, then top attack points
    public IEnumerator GroundedAttack()
    {
        while (true)
        {
            if (isGrounded)
            {
                for (int i = 0; i < leftAttackPoints.Length; i++)
                {
                    var ball = Instantiate(spikedBallPrefab, leftAttackPoints[i]);
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));
                    ball = Instantiate(spikedBallPrefab, rightAttackPoints[i]);
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));
                    yield return new WaitForSeconds(2f);
                }
            }
            yield return null;
        }
    }
    private IEnumerator WaitToLand()
    {
        while (!isGrounded)
        {
            yield return null;
        }
        yield break;
    }

    public IEnumerator SlamAttack()
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 10f);
            ColliderDistance2D distance = hit.collider.Distance(boxCollider);
            float distanceToGround = distance.distance;
            Debug.DrawRay(transform.position, Vector3.down * 10f, Color.green);

            if (distanceToGround < 5f)
            {
                yield return StartCoroutine(MoveUp(15f));
            }
            else
            {
                StopAllCoroutines();
                yield return StartCoroutine(MoveDown(-20f));
            }
            yield return null;
        }
    }

    public IEnumerator MoveUp(float force)
    {
        while (isGrounded)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            yield return null;
        }
        yield break;
    }
    private IEnumerator MoveDown(float force)
    {
        while (!isGrounded)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            yield return null;
        }
        yield return StartCoroutine(SlamAttack());
    }
    private bool CheckForPlayer()
    {
        Collider2D result = Physics2D.OverlapBox(transform.position, new Vector2(50f, 100f), 0, playerLayerMask);
        if (result != null) return true;
        return false;
    }

    private IEnumerator LandingAnimation()
    {
        for(int i = 0; i < landingAnimationSprites.Length; i++)
        {
            spriteRenderer.sprite = landingAnimationSprites[i];
            yield return new WaitForSeconds(1 / 24f);
        }
        yield break;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position, new Vector3(10f, 5f, 0f));
    }
}
