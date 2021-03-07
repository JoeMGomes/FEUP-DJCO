using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public LayerMask hitLayer;

    public GameObject zapp;
    public GameObject grenade;

    public PlayerMana mana;
    public float manaPrimary = 20;
    public float manaSecondary = 45;
    public float damage = 30;
    public float throwForce = 5;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          ShootPrimary();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ShootSecondary();
        }
    }

    private void ShootSecondary()
    {

        if(mana.mana < manaSecondary ) return;
        mana.UseMana(manaSecondary );

        GameObject grenadeInstance = Instantiate(grenade, bulletSpawn.position, Quaternion.identity);
        Rigidbody2D grenRigidBody = grenadeInstance.GetComponent<Rigidbody2D>();
        grenRigidBody.velocity = transform.TransformDirection(bulletSpawn.transform.right * throwForce);
    }

    void ShootPrimary()
    {
        if (mana.mana < manaPrimary) return;
        mana.UseMana(manaPrimary);

        Vector2 shootPosition = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, Camera.main.ScreenToWorldPoint(shootPosition) - bulletSpawn.position, 100, hitLayer);

        if (hit)
        {
            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(hit.point);

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyStats s = hit.collider.gameObject.GetComponent<EnemyStats>();
                s.TakeDamage(damage);
                s.Flinch(transform.position, 2);

            }
        }
        else {

            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(Camera.main.ScreenToWorldPoint(shootPosition) );
        }

    }

}
