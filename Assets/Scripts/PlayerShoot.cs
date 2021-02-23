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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          Shoot();
        }
    }

    void Shoot()
    {
        Vector2 shootPosition = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, Camera.main.ScreenToWorldPoint(shootPosition) - bulletSpawn.position, 100, hitLayer);

        if (hit)
        {
            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(hit.point);
        }
        else {

            Zapp zappScript = Instantiate(zapp, bulletSpawn.position, Quaternion.identity).GetComponent<Zapp>();
            zappScript.ZapTarget(Camera.main.ScreenToWorldPoint(shootPosition) );
        }
    }

}
