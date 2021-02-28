using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject targetPlayer;
    public float chaseDistance = 5.0f; //Distance at wich enemies will start to follow the player
    public float moveSpeed = 3.0f;
    public float meleeDamage = 20;


    public GameObject armParent;
    public GameObject bodyParent;


    private void Awake()
    {
        targetPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Follow player behaviour
        Vector2 playerDist = PlayerDistance();
        if (playerDist.magnitude < chaseDistance)
        {
            transform.Translate(moveSpeed * Time.deltaTime * (playerDist.x < 0 ? 1 : -1), 0f, 0f);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(meleeDamage);
        }
    }

    Vector2 PlayerDistance()
    {
        return transform.position - targetPlayer.transform.position;
    
    }
    private void FixedUpdate()
    {
        Vector2 diff = -PlayerDistance();

        diff.Normalize();
        float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        //Handle arm roation and face the player sprite to the mouse
        armParent.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        bodyParent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //Mouse to the left side of the player
        if (rotation < -90 || rotation > 90)
        {
            if (armParent.transform.eulerAngles.y == 0)
            {
                armParent.transform.localRotation = Quaternion.Euler(180, 0, -rotation);
                bodyParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
