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

    public ItemDatabase itemDatabase;

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

    private void Start() 
    {
        LoadInventory();
        FindAnyObjectByType<UIManager>().UpdateUI();
    }

    void LoadInventory()
    {
        for(int i = 0; i < GameManager.gm.weaponID.Length; i++)
        {
            AddWeapon(itemDatabase.GetWeapon(GameManager.gm.weaponID[i]));
        }
        for(int i = 0; i < GameManager.gm.itemID.Length; i++)
        {
            AddItem(itemDatabase.GetConsumableItem(GameManager.gm.itemID[i]));
        }
        for(int i = 0; i < GameManager.gm.armorID.Length; i++)
        {
            AddArmor(itemDatabase.GetArmor(GameManager.gm.armorID[i]));
        }
        for(int i = 0; i < GameManager.gm.weaponID.Length; i++)
        {
            AddKey(itemDatabase.GetKey(GameManager.gm.keyID[i]));
        }
    }
    
    public void AddWeapon(Weapons weapon)
    {
        weapons.Add(weapon);
    }

    public void AddArmor(Armor armor)
    {
        armors.Add(armor);
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
