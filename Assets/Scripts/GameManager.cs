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
    public static GameManager gm;
    
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
    public bool canBackDash = false;

    private string filePath;

    private void Awake() 
    {
        if(gm == null)
        {
            gm = this;
        }
        else if(gm != null)
        {
            Destroy(gm);
        }

        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";

        Load();
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
            weaponID[i] = Inventory.inventory.weapons[i].weaponID;
        }

        for(int i = 0; i < armorID.Length; i++)
        {
            armorID[i] = Inventory.inventory.armors[i].armorID;
        }

        for(int i = 0; i < keyID.Length; i++)
        {
            keyID[i] = Inventory.inventory.keys[i].keyID;
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
        FindAnyObjectByType<UIManager>().SetMessage("Game Saved!");
    }

    public void Load()
    {
        if(File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = data.health;
            mana = data.mana;
            strength = data.strength;
            playerPosX = data.playerPosX;
            playerPosY = data.playerPosY;
            knowledge = data.knowledge;
            upgradeCost = data.upgradeCost;
            currentArmorID = data.currentArmorID;
            currentWeaponID = data.currentWeaponID;
            canDoubleJump = data.canDoubleJump;
            canBackDash = data.canBackDash;
            itemID = data.itemID;
            weaponID = data.weaponID;
            armorID = data.armorID;
            keyID = data.keyID;
        }
    }
}
