using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Keys key;
    public Sprite doorOpen;

    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

    void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Inventory.inventory.CheckKey(key))
            {
                sprite.sprite = doorOpen;
                boxCollider.enabled = false;
            }
        }
    }
}
