using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] private bool input;
    [SerializeField] private bool canAttack;

    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float force;
    private float duration; // time before next attack

    // Start is called before the first frame update
    void Start()
    {
        duration = 0.5f;
        force = 250f;
        GameObject player = transform.gameObject;
        attackPoint = transform.gameObject.GetComponentsInChildren<Transform>()[1];
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetKeyDown(KeyCode.E);

        if (canAttack && input)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        var attack = Instantiate(attackPrefab, attackPoint.position, Quaternion.identity);
        SpriteRenderer sr = attack.GetComponent<SpriteRenderer>();
        sr.flipX = true;
        // Add a force to the attack
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * force, ForceMode2D.Force);
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }

}
