using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ConsumableItem item;
    private SpriteRenderer sprite;

    private void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.image;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Inventory.inventory.AddItem(item);
            FindAnyObjectByType<UIManager>().UpdateUI();
            FindAnyObjectByType<UIManager>().SetMessage(item.message);
            Destroy(gameObject);
        }
    }
}
