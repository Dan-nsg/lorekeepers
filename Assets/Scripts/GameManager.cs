using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData
{
    public int health;
    public int mana;
    public int strength;
    public float playerPosX, playerPosY;
    public int knowledge;
    public int[] itemID;
    public int[] weaponID;
    public int[] armorID;
    public int[] keyID;
    public int upgradeCost;
    public int currentWeaponID;
    public int currentArmorID;
    public bool canDoubleJump;
    public bool canBackDash;
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    
    public int health = 100;
    public int mana = 50;
    public int strength = 10;
    public float playerPosX, playerPosY;
    public int knowledge;
    public int[] itemID;
    public int[] weaponID;
    public int[] armorID;
    public int[] keyID;
    public int upgradeCost;
    public int currentWeaponID;
    public int currentArmorID;
    public bool canDoubleJump = false;
    public bool canSpinDash = false;

    private string filePath;

    private void Awake() 
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else if(gameManager != null)
        {
            Destroy(gameManager);
        }

        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";
    }

    public void Save()
    {
        Player player = FindAnyObjectByType<Player>();

        itemID = new int[Inventory.inventory.items.Count];
        weaponID = new int[Inventory.inventory.weapons.Count];
        armorID = new int [Inventory.inventory.armors.Count];
        keyID = new int [Inventory.inventory.keys.Count];

        for(int i = 0; i < itemID.Length; i++)
        {
            itemID[i] = Inventory.inventory.items[i].itemID;
        }

        for(int i = 0; i < weaponID.Length; i++)
        {
            itemID[i] = Inventory.inventory.weapons[i].weaponID;
        }

        for(int i = 0; i < armorID.Length; i++)
        {
            itemID[i] = Inventory.inventory.armors[i].armorID;
        }

        for(int i = 0; i < keyID.Length; i++)
        {
            itemID[i] = Inventory.inventory.keys[i].keyID;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        PlayerData data = new PlayerData();

        data.health = player.maxHealth;
        data.mana = player.maxMana;
        data.playerPosX = player.transform.position.x;
        data.playerPosY = player.transform.position.y;
        data.knowledge = player.knowledge;
        data.strength = player.strength;
        data.upgradeCost = upgradeCost;
        if(player.weaponEquipped != null)
            data.currentWeaponID = player.weaponEquipped.weaponID;
        if(player.armor != null)
            data.currentArmorID = player.armor.armorID;
        data.canDoubleJump = player.doubleJumpSkill;
        data.canBackDash = player.dashSkill;

        data.itemID = itemID;
        data.weaponID = weaponID;
        data.armorID = armorID;
        data.keyID = keyID;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Game Saved!");
    }
}
