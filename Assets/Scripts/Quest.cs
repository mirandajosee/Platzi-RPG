using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    public int XPQuest;
    private QuestManager manager;
    private CharacterStats stats;

    public string startText, completeText;

    public bool needsItem;
    public string itemNeeded;

    public bool needsEnemy;
    public string enemyName;
    public int numberOfEnemies;
    private int enemiesKilled;


    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerController>().gameObject.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (needsItem && manager.itemCollected.Equals(itemNeeded))
        {
            manager.itemCollected = "";
            CompleteQuest();
        }

        if (needsEnemy && manager.enemyKilled != null)
        {
            if (needsEnemy && manager.enemyKilled.Equals(enemyName))
            {
                manager.enemyKilled = "";
                enemiesKilled++;
                if (enemiesKilled >= numberOfEnemies)
                {
                    CompleteQuest();
                }
            }
        }

    }

    public void StartQuest()
    {
        manager = FindObjectOfType<QuestManager>();
        manager.ShowQuestText(startText);
    }

    public void CompleteQuest()
    {
        manager.ShowQuestText(completeText);
        manager.questCompleted[questID] = true;
        gameObject.SetActive(false);

        stats.AddExperience(XPQuest);

    }
}
