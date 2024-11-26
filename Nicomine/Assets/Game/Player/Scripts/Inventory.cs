using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum Items
{
    CORK = 1, ANTIDOTE = 2
}

public class Inventory : MonoBehaviour
{

    public TMP_Text AntidoteTMP;
    public TMP_Text CorkTMP;

    private Dictionary<Items, int> inventory = new()
    {
        { Items.CORK, 0 },
        { Items.ANTIDOTE, 0 }
    };

    private void Start()
    {
        SetLabels();
    }

    public int Corks { get => inventory[Items.CORK]; set => SetItem(Items.CORK, value); }
    public int Antidotes { get => inventory[Items.ANTIDOTE]; set => SetItem(Items.ANTIDOTE, value); }

    public void AddItem(Items item, int amount = 1)
    {
        if (amount < 0) throw new ArgumentException("DONNE PLUS QUE 0 PD");
        inventory[item] += amount;
        SetLabels();
    }

    public void RemoveItem(Items item, int amount = 1)
    {
        inventory[item] = Mathf.Max(0, inventory[item] - amount);
        SetLabels();
    }

    private void SetItem(Items item, int count)
    {
        inventory[item] = Mathf.Max(0, count);
        SetLabels();
    }

    private void SetLabels()
    {
        if (AntidoteTMP != null)
        {
            AntidoteTMP.text = inventory[Items.ANTIDOTE].ToString();
        }

        if (CorkTMP != null)
        {
            CorkTMP.text = inventory[Items.CORK].ToString();
        }
    }
}
