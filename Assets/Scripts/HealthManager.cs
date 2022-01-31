using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public bool flashActive = false;
    public Color flashColor;
    public Color regularColor;
    public float flashLength;
    public int numberOfFlashes;
    private Collider2D triggerCollider;
    private SpriteRenderer characterRenderer;
    private QuestManager manager;

    public int expWhenDefeated;
    public string enemyName;

    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        characterRenderer = GetComponent<SpriteRenderer>();
        triggerCollider = GetComponent<Collider2D>();
        manager = FindObjectOfType<QuestManager>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
                manager.enemyKilled = enemyName;

            }

            if (gameObject.CompareTag("Player"))
            {
                sfxManager.playerDead.Play();
            }

            gameObject.SetActive(false);
        }

        if (flashActive && currentHealth>0)
        {
            StartCoroutine(FlashCo());
        }
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if (this.gameObject.CompareTag("Player") && currentHealth>0)
        {
            flashActive = true;
            sfxManager.playerHurt.Play();
        }

    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        /*triggerCollider.enabled = false;*/
        while (temp < numberOfFlashes && currentHealth>0)
        {
            characterRenderer.color = flashColor;
            yield return new WaitForSeconds(flashLength);
            characterRenderer.color = regularColor;
            yield return new WaitForSeconds(flashLength);
            temp++;
        }
        /*triggerCollider.enabled = true;*/
        flashActive = false;
    }
}
