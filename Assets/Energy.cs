using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public GameObject energyIconPrefab;
    public Transform energyPanel;
    public int energy = 0;

    public void Start()
    {
        for (int i = 0; i < energy; i++)
        {
            GameObject icon = Instantiate(energyIconPrefab, energyPanel);
            icon.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject icon = Instantiate(energyIconPrefab, energyPanel);
                        icon.SetActive(true);
        }
            
    }

    public void AddEnergy(int amount)
    {
        energy += amount;

        for (int i = 0; i < amount; i++)
        {
            GameObject icon = Instantiate(energyIconPrefab, energyPanel);
            icon.SetActive(true);
        }
    }
}
