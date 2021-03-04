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
    public float manaUsage = 20;
    public float damage = 30;

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
        Instantiate(grenade, bulletSpawn.position, Quaternion.identity);
    }

    void ShootPrimary()
    {
        if (mana.mana < manaUsage) return;
        mana.UseMana(manaUsage);

        Vector2 shootPosition = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, Camera.main.ScreenToWorldPoint(shootPosition) - bulletSpawn.position, 100, hitLayer);

        if (hit)
        {
            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(hit.point);

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);

            }
        }
        else {

            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(Camera.main.ScreenToWorldPoint(shootPosition) );
        }

    }

}
