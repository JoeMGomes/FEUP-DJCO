using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFloor : MonoBehaviour
{
    public float slowMultiplier = 5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            mov.moveSpeed /= slowMultiplier;
            mov.onWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            mov.moveSpeed *= slowMultiplier;
            mov.onWater = false;
        }
    }
}
