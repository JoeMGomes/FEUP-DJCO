﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodleCrash : MonoBehaviour
{

    public float time = 2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //StartCoroutine(Pickup());
            Pickup();
        }
    }
    void Pickup()
    {
        // cool efect ??

        // apply efect to the enemies (numb?)
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
