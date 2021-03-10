using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodleCrash : MonoBehaviour
{

    public float time = 2f;
    public float scoreValue = 20;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            other.GetComponent<Score>().IncrementScore(scoreValue);
        }
    }
    void Pickup()
    {
        // cool efect ??
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerGrabPowerUp);

        // apply efect to the enemies (numb)
        ExecuteEffectOnEnemies();

        Destroy(gameObject);
    }

    void ExecuteEffectOnEnemies()
    {
        GameObject[] students = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject student in students)
        {
            student.GetComponent<EnemyBehaviour>().Numb(time);
        }
    }
}
