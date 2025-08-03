using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public int posPlayerOnBoard = 0;
    public bool IsDoneMoving { get; private set; } = true;
    public CombatManager combatManager;
    public int loop = 0;
    public ShopManager shopManager;

    private void Awake()
    {
        instance = this;
        combatManager = FindAnyObjectByType<CombatManager>();
        shopManager = FindAnyObjectByType<ShopManager>();
    }

    private void Start()
    {
        posPlayerOnBoard = 0;
    }

    public void Move(int points)
    {
        StartCoroutine(WaitToContinueMovement(points));
    }

    public void Reset()
    {
        posPlayerOnBoard = 0;
        Vector2 pos = Board.instance.squares[posPlayerOnBoard].transform.position;
        transform.position = pos;
        loop = 0;
        
    }
    private IEnumerator WaitToContinueMovement(int points)
    {
        for (int i = 1; i <= points; i++)
        {
            posPlayerOnBoard += 1;
            if (posPlayerOnBoard >= Board.instance.squares.Count)
            {
                posPlayerOnBoard = 0;
                loop += 1;
            }
            Vector2 pos = Board.instance.squares[posPlayerOnBoard].transform.position;
            SoundSFXManager.instance.PlayMovementSound();
            transform.position = pos;
            yield return new WaitForSeconds(0.15f);
        }
        IsDoneMoving = true;


    }
    public void TriggerSquare()
    {
        GameObject square = Board.instance.squares[posPlayerOnBoard];
        if (loop >= 1)
        {
            combatManager.StartCombat(MonsterDifficulty.MiniBoss, loop);
            MoveCamera.Instance.MoveBattle();
            return;

        }
        if (square.name == MonsterDifficulty.MonsterSquareEasy.ToString())
        {
            combatManager.StartCombat(MonsterDifficulty.MonsterSquareEasy, loop);
            MoveCamera.Instance.MoveBattle();

        }
        else if (square.name == MonsterDifficulty.MonsterSquareNormal.ToString())
        {
            combatManager.StartCombat(MonsterDifficulty.MonsterSquareNormal, loop);
            MoveCamera.Instance.MoveBattle();


        }
        else if (square.name == MonsterDifficulty.MonsterSquareHard.ToString())
        {
            combatManager.StartCombat(MonsterDifficulty.MonsterSquareHard, loop);
            MoveCamera.Instance.MoveBattle();


        }
        else if (square.name == "ShopSquare")
        {
            ShopManager.instance.OpenShop();
        }



    }
    
}
