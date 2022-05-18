using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;
    private int index;

    void Start()
    {
        index = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = sprites[index];
        index = (index + 1) % sprites.Length;
    }
}
