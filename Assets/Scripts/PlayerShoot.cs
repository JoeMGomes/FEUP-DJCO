using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public LayerMask hitLayer;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Vector2 shootPosition = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, Camera.main.ScreenToWorldPoint(shootPosition) - bulletSpawn.position, 100, hitLayer);

        if (hit)
        {
            Debug.Log(hit.transform.name);
            lineRenderer.SetPosition(0, bulletSpawn.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else {

            lineRenderer.SetPosition(0, bulletSpawn.position);
            Vector3 infiniteTrail = Camera.main.ScreenToWorldPoint(shootPosition) - bulletSpawn.position;
            infiniteTrail.Scale(new Vector3(100, 100, 0));


            lineRenderer.SetPosition(1, infiniteTrail );
        }
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;

    }
}
