using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSaw : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int spriteIndex;
    [SerializeField] private bool spinning;
    private Movement movement;
    private float time = 0f;
    private float movementFactor;
    private enum Movement
    {
        Left,
        Right,
        None
    }

    private void Awake()
    {
        spinning = true;
        spriteIndex = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementFactor = 1f / 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int mvmtIndex = (int)(time * movementFactor % 2);
        movement = (Movement)mvmtIndex;
    }

    private IEnumerator Rotate()
    {
        while (spinning)
        {
            spriteIndex = (spriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[spriteIndex];
            yield return new WaitForSeconds(1f / 24);
        }
    }

    private IEnumerator Move()
    {
        while (movement != Movement.None)
        {
            Vector3 position = transform.position;
            Vector3 move = Vector3.zero;
            float scale = 1f / 24;

            switch (movement)
            {
                case Movement.Left:
                    move = Vector3.left * scale;
                    break;
                case Movement.Right:
                    move = Vector3.right * scale;
                    break;
            }
            transform.position = position + move;
            yield return new WaitForSeconds(1f / 24);
        }
    }
}
