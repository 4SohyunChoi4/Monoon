using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

public enum InterfaceType
{
    Inventory,
    Equipment,
    Face
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObjects : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public InterfaceType type;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots;  } }

    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0)
            return false;
        InventorySlot slot = FindItemInventory(_item);
        if (!database.Items[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }
    public InventorySlot FindItemInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].updateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.updateSlot(item1.item, item1.amount);
            item1.updateSlot(temp.item, temp.amount);
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        Debug.Log("save함");
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].updateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
            Debug.Log("load 로드함");
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
        Debug.Log("clear함");
    }

}
[System.Serializable]
public class Inventory
{
    //public List<InventorySlot> Items = new List<InventorySlot>();
    public InventorySlot[] Slots = new InventorySlot[33];

    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].updateSlot(new Item(), 0);
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public Item item = new Item();
    public int amount;

    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.Items[item.Id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        updateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        updateSlot(_item, _amount);
    }
        public void updateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void AddAmount(int value)
    {
        updateSlot(item, amount += value);
    }
    public void RemoveItem()
    {
        updateSlot(new Item(), 0);
    }

    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
                return true;
        }
        return false;
    }
}