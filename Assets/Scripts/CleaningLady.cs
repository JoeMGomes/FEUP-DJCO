using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningLady : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);

        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerMovement>().Flinch(gameObject.transform.position);

        }
    }

}
