using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;// = new ItemObject[33] ;
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

public void UpdateID(){
    for(int i =0 ;i< Items.Length ; i++){
        if (Items[i].data.Id != i){
            Items[i].data.Id = i;
        }
    }
}

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        UpdateID();
    }   

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, ItemObject>();

    }
}
