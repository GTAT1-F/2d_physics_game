using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(1 / 24f);
        }
        Destroy(gameObject);
        yield break;
    }
}
