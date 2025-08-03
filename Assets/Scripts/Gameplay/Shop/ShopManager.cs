using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public List<ItemShop> itemShops = new List<ItemShop>();
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public CombatManager combatManager; 
    public List<Skill> skillShop = new List<Skill>();
    public List<GameObject> skillPrefabs = new List<GameObject>();
    public List<TextMeshProUGUI> TextMeshProUguis = new List<TextMeshProUGUI>();
    public GameObject SkillReplacePanel;
    public Skill skillReplace;
    public int coins = 0;
    public GameObject shop;
    public TextMeshProUGUI coinsText;
    public Button roll;



    private void Start()
    {
        instance = this;
    }

    public void CloseShop()
    {
        roll.interactable = true;
        shop.gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        GenerateShops();
        roll.interactable = false;

        shop.SetActive(true);
    }

    public void GenerateShops()
    {
        Shuffle(itemShops);
        Shuffle(skillShop);
        foreach (GameObject shop in itemPrefabs)
        {
            shop.GetComponent<Image>().sprite = itemShops[Random.Range(0, itemShops.Count)].itemSprite;
        }
        for (int i = 0; i < skillPrefabs.Count; i++)
        {
            skillPrefabs[i].name = skillShop[i].skillName;
            TextMeshProUguis[i].text = skillShop[i].skillName;
        }
        coinsText.text = coins.ToString();
    }
    public void BuySkill(GameObject skill)
    {
        if (coins == 0) return;
        foreach (var skillS in skillShop)
        {
            if (skill.name == skillS.skillName)
            {
                coins -= 1;
                coinsText.text = coins.ToString();
                skill.SetActive(false);
                skillReplace = skillS;
                SkillReplacePanel.SetActive(true);
            }

        }
    }

    public void ReplaceSkill(int skill)
    {
        combatManager.playerData.EquipNewSkill(skillReplace, skill);
        SkillReplacePanel.SetActive(false);
    }

    public void BuyItem(GameObject shop)
    {
        if (coins == 0) return;

        foreach (var item in itemShops)
        {
            if (shop.GetComponent<Image>().sprite == item.itemSprite)
            {
                combatManager.playerData.AddItem(item.item);
                coins -= 1;
                coinsText.text = coins.ToString();
                shop.SetActive(false);
            }

        }
    }
    
    private void Shuffle<T>(IList<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T tmp = list[n];
            list[n] = list[k];
            list[k] = tmp;
        }
    }
}
