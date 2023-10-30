using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Weapons> weapons;
    public List<ConsumableItem> consumableItems;
    public List<Armor> armors;
    public List<Keys> keys;

    public Weapons GetWeapon(int itemID)
    {
        foreach(var item in weapons)
        {
            if(item.weaponID == itemID)
                return item;
        }
        return null;
    }

    public ConsumableItem GetConsumableItem(int itemID)
    {
        foreach(var item in consumableItems)
        {
            if(item.itemID == itemID)
                return item;
        }
        return null;
    }

    public Armor GetArmor(int itemID)
    {
        foreach(var item in armors)
        {
            if(item.armorID == itemID)
                return item;
        }
        return null;
    }

    public Keys GetKey(int itemID)
    {
        foreach(var item in keys)
        {
            if(item.keyID == itemID)
                return item;
        }
        return null;
    }
}
