using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] numberSprites;
    private SpriteRenderer _spriteRenderer;
    public bool status = true;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = numberSprites[0];
    }
    void Update()
    {
        if (status)
        {
            _spriteRenderer.sprite = numberSprites[0];
        }
        else 
        {
            _spriteRenderer.sprite = numberSprites[1];
        }

    }
}
