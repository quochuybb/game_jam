using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct SpriteMapping
{
    public SquareType key;    
    public Sprite sprite;
}

public class SpriteManager : MonoBehaviour
{
    [SerializeField]
    private SpriteMapping[] spriteMappings;
    private Dictionary<SquareType, Sprite> spriteDict;
    public static SpriteManager instance;

    void Awake()
    {
        spriteDict = new Dictionary<SquareType, Sprite>();

        foreach (var mapping in spriteMappings)
        {
            if (mapping.sprite != null)
                spriteDict[mapping.key] = mapping.sprite;
        }
        instance = this;
    }

    public Sprite GetSprite(SquareType spriteKey)
    {
        if (spriteDict.TryGetValue(spriteKey, out var found))
            return found;
        return null;
    }
}

