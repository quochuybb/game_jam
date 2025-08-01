using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquareType
{
    MonsterSquareEasy,
    MonsterSquareNormal,
    MonsterSquareHard,
    ItemSquare,
    ShopSquare,
    BuffSquare,
    TombMark,
    Start
}
public class Square : MonoBehaviour
{
    public int id;
    public SquareType squareType;
    public Vector3 position;


}
