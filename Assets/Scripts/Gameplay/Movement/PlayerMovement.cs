using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }

    public void Move(int points)
    {
        Vector2 pos = BoardManager.instance.squares[points].transform.position;
        transform.position = pos;
    }
    
}
