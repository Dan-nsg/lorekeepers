using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public Keys key;

    private SpriteRenderer sprite;

    private void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = key.keyImage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            Inventory.inventory.AddKey(key);
            FindAnyObjectByType<UIManager>().SetMessage(key.keyMessage);
            Destroy(gameObject);
        }
    }
}
