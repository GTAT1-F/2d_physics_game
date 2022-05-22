using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] private float trampolineForce;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        trampolineForce = 10f;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        // Applies a force to the game object that collided with this if that game object has a rigidbody2d component attached
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if(rb)
        {
            StartCoroutine(Animate());
            rb.AddForce(new Vector2(0, trampolineForce), ForceMode2D.Impulse);
        }
    }

    private IEnumerator Animate()
    {
        spriteIndex = 0;
        while(spriteIndex < sprites.Length)
        {
            sr.sprite = sprites[spriteIndex];
            spriteIndex++;
            yield return new WaitForSeconds(1/24f);
        }
        yield break;
    }
}
