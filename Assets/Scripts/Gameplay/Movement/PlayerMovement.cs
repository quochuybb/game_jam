using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public int posPlayerOnBoard = 0;
    public bool IsDoneMoving { get; private set; } = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        posPlayerOnBoard = 0;
    }

    public void Move(int points)
    {
        StartCoroutine(WaitToContinueMovement(points));
    }
    
    
    private IEnumerator WaitToContinueMovement(int points)
    {
        for (int i = 1; i <= points; i++)
        {
            posPlayerOnBoard += 1;
            if (posPlayerOnBoard >= BoardManager.instance.squares.Count)
            {
                posPlayerOnBoard = 0;
            }
            Vector2 pos = BoardManager.instance.squares[posPlayerOnBoard].transform.position;
            SoundSFXManager.instance.PlayMovementSound();
            transform.position = pos;
            yield return new WaitForSeconds(0.25f);
        }
        IsDoneMoving = true;


    }
    
}
