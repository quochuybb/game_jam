using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in ShopManager.instance.itemShops)
        {
            if (gameObject.GetComponent<Image>().sprite == item.itemSprite)
            {
                text.text = item.item.description;
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}

