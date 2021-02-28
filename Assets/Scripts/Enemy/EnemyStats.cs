using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GameObject targetPlayer;
    public float health = 100;
    public float scoreValue = 20;
    private void Awake()
    {
        targetPlayer = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            targetPlayer.GetComponent<Score>().IncrementScore(scoreValue);
            Destroy(gameObject);
        }   
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
