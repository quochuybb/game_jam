using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    private Square square;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Square square)
    {
        this.square = square;
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        spriteRenderer.sprite = SpriteManager.instance.GetSprite(this.square.squareType);
    }
    
}
