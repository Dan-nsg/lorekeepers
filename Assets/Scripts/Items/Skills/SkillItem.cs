using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public PlayerSkills skills;
    public string message;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.SetPlayerSkill(skills);
            FindAnyObjectByType<UIManager>().SetMessage(message);
            Destroy(gameObject);
        }
    }
}
