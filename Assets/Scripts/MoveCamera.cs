using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform targetBattle;
    public Transform targetBoard;
    public static  MoveCamera Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveBattle()
    {
        gameObject.transform.position = targetBattle.position;
    }
    public void MoveBoard()
    {
        gameObject.transform.position = targetBoard.position;
    }
    
}
