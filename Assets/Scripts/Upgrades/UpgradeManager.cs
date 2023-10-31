using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI[] attributesText;
    public GameObject upgradePanel;

    private bool upgradeActive;
    private Player player;
    private int cursorIndex;

    private void Start() 
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            upgradeActive = !upgradeActive;
            cursorIndex = 0;
            UpdateText();
            if(upgradeActive)
            {
                upgradePanel.SetActive(true);
            }
            else
            {
                upgradePanel.SetActive(false);
            }
        }

        if(upgradeActive)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpdateText();
                cursorIndex++;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpdateText();
                cursorIndex--;
            }

            if(cursorIndex == 0)
            {
                attributesText[0].text = "Life: " + player.maxHealth + ">" + (player.maxHealth + (player.maxHealth * 0.1f));
                attributesText[0].color = Color.yellow;
            }
            else if(cursorIndex == 1)
            {
                attributesText[1].text = "Mana: " + player.maxMana + ">" + (player.maxMana + (player.maxMana * 0.1f));
                attributesText[1].color = Color.yellow;
            }
            else if(cursorIndex == 2)
            {
                attributesText[2].text = "Strength: " + player.strength + ">" + (player.strength + (player.strength * 0.1f));
                attributesText[2].color = Color.yellow;
            }
        }
    }

    private void UpdateText()
    {
            upgradeCostText.text = "Knowledge cost: " + GameManager.gm.upgradeCost + " Knowledge: " + player.knowledge;
            attributesText[0].text = "Life: " + player.maxHealth;
            attributesText[1].text = "Mana: " + player.maxMana;
            attributesText[2].text = "Strength: " + player.strength;
            for(int i = 0; i < attributesText.Length; i++)
            {
                attributesText[i].color = Color.white;
            }
    }
}
