using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;
    private int index;
    private float time;

    void Start()
    {
        time = 0f;
        index = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        sr.sprite = sprites[index];
        index = (int)(time * 8 % sprites.Length);
    }
}
