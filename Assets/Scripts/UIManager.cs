using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Text playerHealthText;
    public HealthManager playerHealthManager;

    public Text playerExpText;
    public CharacterStats playerStats;

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.currentHealth;

        StringBuilder sb = new StringBuilder("HP: ");
        sb.Append(playerHealthManager.currentHealth);
        sb.Append("/");
        sb.Append(playerHealthManager.maxHealth);

        playerHealthText.text = sb.ToString();

        StringBuilder sb2 = new StringBuilder("LEVEL: ");
        sb2.Append(playerStats.currentLevel);
        sb2.AppendLine();
        sb2.Append("EXP: ");
        sb2.Append(playerStats.currentExp);
        sb2.Append("/");
        sb2.Append(playerStats.expToLevelUp[playerStats.currentLevel]);

        playerExpText.text = sb2.ToString();

    }
}
