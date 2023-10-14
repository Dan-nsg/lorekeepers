using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    public List<Weapons> weapons;
    public List<Keys> keys;
    public List<ConsumableItem> items;
    public List<Armor> armors;

    void Awake() 
    {
        if(inventory == null)
        {
            inventory = this;
        }
        else if(inventory != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public void AddWeapon(Weapons weapon)
    {
        weapons.Add(weapon);
    }

    public void AddKey(Keys key)
    {
        keys.Add(key);
    }

    public bool CheckKey(Keys key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if(keys[i] == key)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(ConsumableItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(ConsumableItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] == item)
            {
                items.RemoveAt(i);
                break;
            }
        }
    }

    public int CountItems(ConsumableItem item)
    {
        int numberOfItems = 0;

        for (int i = 0; i < items.Count ; i++)
        {
            if(item == items[i])
            {
                numberOfItems++;
            }
        }
        
        return numberOfItems;
    }
}
