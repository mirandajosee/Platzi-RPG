using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    public GameObject hurtAnimation;
    public GameObject hitZone;
    public GameObject damageNumber;

    private CharacterStats stats;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int totalDamage = damage;
            if (stats != null)
            {
                totalDamage += stats.strengthLevels[stats.currentLevel];
            }

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            Instantiate(hurtAnimation, hitZone.transform.position, hitZone.transform.rotation);
            var clone = (GameObject)Instantiate(damageNumber, hitZone.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints=totalDamage;
        }
    }
}
