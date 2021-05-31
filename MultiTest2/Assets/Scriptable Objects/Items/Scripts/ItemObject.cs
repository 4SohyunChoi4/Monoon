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
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}
