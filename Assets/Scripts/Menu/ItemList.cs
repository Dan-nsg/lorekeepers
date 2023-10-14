using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public ConsumableItem consumableItem;
    public Weapons weapon;
    public Keys key;
    public Armor armor;

    public void SetUpItem(ConsumableItem item)
    {
        consumableItem = item;
        image.sprite = consumableItem.image;
        text.text = consumableItem.itemName;
    }

    public void SetUpKey(Keys item)
    {
        key = item;
        image.sprite = key.keyImage;
        text.text = key.keyName;
    }

    public void SetUpWeapon(Weapons item)
    {
        weapon = item;
        image.sprite = weapon.weaponImage;
        text.text = weapon.weaponName;
    }

    public void SetUpArmor(Armor item)
    {
        armor = item;
        image.sprite = armor.armorImage;
        text.text = armor.armorName;
    }
}
