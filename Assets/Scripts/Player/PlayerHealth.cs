using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if(health < 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerDie);
            GameObject.Find("Level Manager").GetComponent<LevelManager>().LoseGame();
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerHit);

    }
}
