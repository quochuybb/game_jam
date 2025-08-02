using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    
    public void Spawn(Vector3 position)
    {
        GameObject player = Instantiate(playerPrefab, position, Quaternion.identity);
    }
}
