using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MoveEvent moveEvent;
    private void Awake()
    {
        moveEvent.AddListener(Move);
    }

    public void Move(int points)
    {
        
    }
    
}
