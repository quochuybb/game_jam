using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    public Dice dice1;
    public Dice dice2;
    public Button roll;
    public static DiceController instance;

    private void Awake()
    {
        instance = this;
    }

    public void RollBothDicesInOrder()
    {
        StartCoroutine(RollInOrderCoroutine());
        roll.interactable = false;
    }

    private IEnumerator RollInOrderCoroutine()
    {
        int result1 = 0;
        int result2 = 0;

        yield return StartCoroutine(dice1.RollTheDice((result) => result1 = result));
        PlayerMovement.instance.Move(result1);
        yield return new WaitUntil(() => PlayerMovement.instance.IsDoneMoving);

        yield return StartCoroutine(dice2.RollTheDice((result) => result2 = result));
        PlayerMovement.instance.Move(result2);
        yield return new WaitUntil(() => PlayerMovement.instance.IsDoneMoving);
        yield return new WaitForSeconds(1f);
        PlayerMovement.instance.TriggerSquare();


    }
}
