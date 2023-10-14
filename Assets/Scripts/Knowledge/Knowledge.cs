using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    public int knowledge;
    public static Knowledge instance;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }    
        else if(this != instance)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.knowledge += knowledge;
            FindAnyObjectByType<UIManager>().UpdateUI();
            gameObject.SetActive(false);
        }
    }
}
