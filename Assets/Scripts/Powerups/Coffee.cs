using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{

    public float time = 10f;
    public float scoreValue = 20;
    public float multiplier = 3f; // mana regen multiplier

    public SpriteRenderer sprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup());
            other.GetComponent<Score>().IncrementScore(scoreValue);
        }
    }
    IEnumerator Pickup()
    {
        // cool efect ??

        // apply effect to player's mana regen
        PlayerMana mana = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMana>();
        mana.manaRegen *= multiplier;

        // disables powerup collider and sprite
        GetComponent<Collider2D>().enabled = false;
        sprite.enabled = false;

        // wait until effect ends and go back to normal
        yield return new WaitForSeconds(time);
        mana.manaRegen /= multiplier;
        Destroy(gameObject);
    }
}
