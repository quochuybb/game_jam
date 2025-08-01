using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFearture : MonoBehaviour
{
    public Sprite[] numberSprites;
    private SpriteRenderer _spriteRenderer;
    public int randomIndex = 0;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = numberSprites[randomIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomDice();
        }
    }
    public int RandomDice()
    {
        StartCoroutine(RollDice()); 
        return randomIndex+1;
    }
    
    private IEnumerator RollDice()
    {
        if (numberSprites.Length == 6)
        {
            for (int i = 0; i < 10; i++)
            {
                randomIndex = Random.Range(0, 6);
                _spriteRenderer.sprite = numberSprites[randomIndex];
                yield return new WaitForSeconds(0.1f);
            }
            randomIndex = Random.Range(0, 6);
            _spriteRenderer.sprite = numberSprites[randomIndex];
        }
        else
        {
            Debug.LogError("skibidi");
        }
    }
}