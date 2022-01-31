using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class QuestTrigger : MonoBehaviour
{
    private QuestManager manager;
    public int questID;
    public bool startPoint, endPoint;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /* para completar una vez el quest, de lo contrario se saca el siguiente if*/
            if (!manager.questCompleted[questID])
            {
                if(startPoint && !manager.quests[questID].gameObject.activeInHierarchy)
                {
                    manager.quests[questID].gameObject.SetActive(true);
                    manager.quests[questID].StartQuest();
                }
                if(endPoint && manager.quests[questID].gameObject.activeInHierarchy)
                {
                    manager.quests[questID].CompleteQuest();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
