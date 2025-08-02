using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemTags
{
    skills,
    buff,
    diceCard,
    souls,
} 
public class Items : MonoBehaviour
{
    private Sprite _sprite;
    private string _name;
    private string _description;
    private int _cost;
}
