using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Default
}
public abstract class ItemObject : MonoBehaviour
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;


}
