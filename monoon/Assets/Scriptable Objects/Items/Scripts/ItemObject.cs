using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Helmet,
    Face,
    Weapon,
    Shield,
    Boots,
    Default,
    Food,

}
/*
public enum Attributes
{
   
}
*/
[CreateAssetMenu(fileName ="New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;

    public GameObject characterDisplay;

    public bool stackable = true;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
    }
}
