using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public Square square;
    public SpriteRenderer spriteRenderer;

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
