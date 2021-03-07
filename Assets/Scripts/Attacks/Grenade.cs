using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float explosionTime = 1.5f;
    public float radius = 3.0f;
    public float remainingTime;
    public float damage = 30f;
    public LayerMask damageMask;
    public GameObject explosion;

    private void Start()
    {
        remainingTime = explosionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime < 0)
        {
            Explode();
        }

        remainingTime -= Time.deltaTime;
    }

    void Explode()
    {
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, damageMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyStats s = hitCollider.gameObject.GetComponent<EnemyStats>();
                s.TakeDamage(damage);
                s.Flinch(transform.position);
            }
        }
        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(g, g.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

}
