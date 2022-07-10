using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int itemCost;
    [SerializeField] string itemName;
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] bool isDefaultItem;
    [SerializeField] int itemIndex;

    bool isPurchased;
    bool isEquipped;

    private void Start()
    {
        GetPlayerPrefsBool();
    }

    private void Update()
    {
        GetPlayerPrefsBool();
        ItemDefault();
        ItemPurchased();
    }

    private void ItemPurchased()
    {
        //Purchase
        if (isPurchased)
        {
            if (isEquipped)
            {
                buttonText.text = "Equipped";
                PlayerPrefs.SetInt("PlayerType", itemIndex);
            }
            else
            {
                buttonText.text = "Equip";
            }
        }
    }

    private void ItemDefault()
    {
        //Default
        if (!isDefaultItem)
        {
            if (!isPurchased)
            {
                PlayerPrefs.SetInt(itemName, itemCost);
                itemText.text = itemCost.ToString() + " Gold Coin";
            }
            else
            {
                itemText.text = "Purchased";
            }
        }
        else
        {
            PlayerPrefs.SetInt(itemName + "Bool", 1);
            itemText.text = "Default Item";
        }
    }

    private void GetPlayerPrefsBool()
    {
        int boolPurchaseInt = PlayerPrefs.GetInt(itemName + "Bool");
        if (boolPurchaseInt == 1)
        {
            isPurchased = true;
            int boolEquipInt = PlayerPrefs.GetInt(itemName + "Use");
            if (boolEquipInt == 0)
            {
                isEquipped = false;
                buttonText.text = "Equip";
            }
            else if (boolEquipInt == 1)
            {
                isEquipped = true;
                buttonText.text = "Equipped";
            }
        }
        else if (boolPurchaseInt == 0)
        {
            isPurchased = false;
            buttonText.text = "Buy";
        }
    }

    public void IsPurchasePressed()
    {
        if (isPurchased)
        {
            EquipClick();
        }
        else
        {
            PurchaseClick();
        }
    }

    private void EquipClick()
    {
        int isEquippedInt = PlayerPrefs.GetInt(itemName + "Use");
        if (isEquippedInt != 1)
        {
            ShopItem[] items = FindObjectsOfType<ShopItem>();
            foreach (ShopItem item in items)
            {
                item.UnEquipItems();
            }
            PlayerPrefs.SetInt(itemName + "Use", 1);
        }
    }

    private void PurchaseClick()
    {
        if (!isDefaultItem)
        {
            if (!isPurchased)
            {
                if (PlayerPrefs.GetInt(itemName) < PlayerPrefs.GetInt("GoldCoin"))
                {
                    PlayerPrefs.SetInt(itemName + "Bool", 1);
                    PlayerPrefs.SetInt(itemName + "Use", 0);
                    PlayerPrefs.SetInt("GoldCoin", PlayerPrefs.GetInt("GoldCoin") - itemCost);
                    isPurchased = true;
                }
            }
        }
    }

    public void UnEquipItems()
    {
        Debug.Log("MessageCalled");
        PlayerPrefs.SetInt(itemName + "Use", 0);
    }
}
