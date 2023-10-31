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
    private PlayerControls playerControls;

    private void Awake() 
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }

    private void Start() 
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Update() 
    {
        if(playerControls.Gameplay.OpenUpgrades.triggered)
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
            if(playerControls.Gameplay.UIDown.triggered)
            {
                UpdateText();
                cursorIndex++;
            }
            else if(playerControls.Gameplay.UIUp.triggered)
            {
                UpdateText();
                cursorIndex--;
            }

            if(cursorIndex == 0)
            {
                attributesText[0].text = "Life: " + player.maxHealth + ">" + Mathf.RoundToInt(player.maxHealth + (player.maxHealth * 0.1f));
                attributesText[0].color = Color.yellow;
            }
            else if(cursorIndex == 1)
            {
                attributesText[1].text = "Mana: " + player.maxMana + ">" + Mathf.RoundToInt(player.maxMana + (player.maxMana * 0.1f));
                attributesText[1].color = Color.yellow;
            }
            else if(cursorIndex == 2)
            {
                attributesText[2].text = "Strength: " + player.strength + ">" + Mathf.RoundToInt(player.strength + (player.strength * 0.1f));
                attributesText[2].color = Color.yellow;
            }

            if(Input.GetButtonDown("Submit") && player.knowledge >= GameManager.gm.upgradeCost)
            {
                player.knowledge -= GameManager.gm.upgradeCost;
                GameManager.gm.upgradeCost += (GameManager.gm.upgradeCost / 2);
                if(cursorIndex == 0)
                {
                    player.maxHealth += (int)(player.maxHealth * 0.1f);
                }
                else if(cursorIndex == 1)
                {
                    player.maxMana += (int)(player.maxMana * 0.1f);
                }
                else if(cursorIndex == 2)
                {
                    player.strength += (int)(player.strength * 0.1f);
                }

                UpdateText();
                FindAnyObjectByType<UIManager>().UpdateUI();
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
