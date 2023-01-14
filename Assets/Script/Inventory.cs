using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item Data", order = 51)]
public class Inventory : ScriptableObject
{
    public int ID;
    public string Name;
    public int bonus;
    public int tier;
    public string color;
    public Sprite image;
    public string slot;
    public string yetiType;
    public Sprite Image;
    public string Category;

    private int inventoryCount;
    public Inventory(int ID, int count)
    {
        this.ID = ID;
        this.inventoryCount = count;
    }

    public void SetInventoryItem(Inventory inv)
    {
        this.name = inv.name;
        this.bonus = inv.bonus;
        this.tier = inv.tier;
        this.color = inv.color;
        this.image = inv.image;
        this.slot = inv.slot;
        this.yetiType = inv.yetiType;
        this.Image = inv.Image;
        this.Category = inv.Category;
    }

    public int GetInventoryCount()
    {
        return inventoryCount;
    }

    public void SetInventoryCount(int value)
    {
        inventoryCount = value;
    }

    public void SellItem(int value)
    {
        inventoryCount -= value;
    }
}
