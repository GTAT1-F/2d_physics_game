using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using bsm = BossStateMachine;


public class Boss : MonoBehaviour
{
    [SerializeField] GameObject spikedBallPrefab;
    private Transform bossTransform => transform;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Transform[] leftAttackPoints;
    [SerializeField] Transform[] rightAttackPoints;

    [SerializeField] private int health;
    [SerializeField] private bool isGrounded;
    [SerializeField] public float ballForce;

    [SerializeField] TextMeshPro damageText;
    [SerializeField] bsm.BossStateMachine stateMachine;

    //public List<IEnumerator> possibleActions;
    public List<string> possibleActions;
    public string currentAction;
    //public IEnumerator currentAction;

    [SerializeField] private float attackTimer;
    public float attackDuration;
    [SerializeField] private bool attackInProgress;
    [SerializeField] private bool playerInBox;



    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        ballForce = 100f;
        attackTimer = attackDuration - 0.5f;
        isGrounded = false;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine = GetComponentInChildren<bsm.BossStateMachine>();
        possibleActions = new List<string>();
        currentAction = null;
    }

    // Update is called once per frame
    void Update()
    {
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
            else if(health > 0)
            {
                stateMachine.Trigger(bsm.BossTransitions.AttackPhase2);
            }
            else if (health <= 0)
            {
                stateMachine.Trigger(bsm.BossTransitions.Death);
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if boss is on the ground
        isGrounded = boxCollider.IsTouchingLayers(Physics2D.AllLayers);

        // Check if player is in radius
        playerInBox = CheckForPlayer();


        // Check if attack is finished
        attackInProgress = attackTimer <= attackDuration;

        // Choose an attack of the attacks possible in boss's current state after the previous attack has played for attackDuration
        if (!attackInProgress)
        {
            int actionsCount = possibleActions.Count;
            if (currentAction != null)
            {
                StopAllCoroutines();                
            }
            if (actionsCount > 0)
            {
                currentAction = possibleActions[Random.Range(0, actionsCount)];
                StartCoroutine(currentAction);
            }
        }

        // Update the timer
        if (!attackInProgress)
        {
            attackTimer = 0f;
            attackInProgress = false;
        }
        else
        {
            attackTimer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        if(go.layer == 3) // Ground layer
        {
            isGrounded = true;
            //StartCoroutine(TakeDamage(0));
        }
    }

    // Instantiates a spiked ball at the center attack point on each side
    public void BallAttack()
    {
        Transform leftAttackPoint = leftAttackPoints[1];
        var ball = Instantiate(spikedBallPrefab, leftAttackPoint);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));

        Transform rightAttackPoint = rightAttackPoints[1];
        ball = Instantiate(spikedBallPrefab, rightAttackPoint);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;

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
            yield return StartCoroutine(MoveUp(8f));
            yield return StartCoroutine(WaitToLand());
            if(isGrounded) BallAttack();
        }  
    }

    // Sends out balls from the bottom, then center, then top attack points
    public IEnumerator GroundedAttack()
    {
        while (true)
        {
            for (int i = 0; i < leftAttackPoints.Length; i++)
            {
                yield return new WaitForSeconds(1f);
                var ball = Instantiate(spikedBallPrefab, leftAttackPoints[i]);
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));
                ball = Instantiate(spikedBallPrefab, rightAttackPoints[i]);
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballForce * ball.transform.right.x, 0));
            }
        }
    }

    public IEnumerator MoveUp(float force)
    {
        while (isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, force));
            yield return null;
        }
    }

    private IEnumerator WaitToLand()
    {
        while (true)
        {
            isGrounded = boxCollider.IsTouchingLayers(Physics2D.AllLayers);
            if (isGrounded) yield break;
            yield return null;
        }
    }

    public IEnumerator SlamAttack()
    {
        float distanceToGround = 0f;

        while (distanceToGround < 10f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 50f);
            ColliderDistance2D distance = hit.collider.Distance(boxCollider);
            distanceToGround = distance.distance;
            Debug.DrawRay(transform.position, Vector3.down * 10f, Color.red);
            yield return StartCoroutine(MoveUp(24f));
        }
        StopCoroutine(MoveUp(24f));
        yield return StartCoroutine(MoveDown(-40f));
        //yield return StartCoroutine(SlamAttack());
    }
    private IEnumerator MoveDown(float force)
    {
        while(!isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, force));
            yield return null;
        }
        //yield break;
        yield return StartCoroutine(SlamAttack());
    }
    private bool CheckForPlayer()
    {
        int layerMask = LayerMask.GetMask("Player");

        Collider2D result = Physics2D.OverlapBox(transform.position, new Vector2(50f, 100f), 0, layerMask);
        if (result != null) return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position, new Vector3(10f, 5f, 0f));
    }
}
