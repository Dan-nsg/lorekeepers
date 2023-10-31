using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public Weapons weapon;
    
    private SpriteRenderer sprite;

    void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = weapon.weaponImage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.AddWeapon(weapon);
            Inventory.inventory.AddWeapon(weapon);
            FindObjectOfType<UIManager>().SetMessage(weapon.collectWeaponMessage);
            Destroy(gameObject);
        }
    }
}
