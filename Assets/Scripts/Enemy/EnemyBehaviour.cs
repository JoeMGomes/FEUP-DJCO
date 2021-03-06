using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject targetPlayer;
    public float chaseDistance = 5.0f; //Distance at wich enemies will start to follow the player
    public float spotTime = 0.7f;
    public float moveSpeed = 3.0f;
    public float meleeDamage = 20;


    public GameObject armParent;
    public GameObject bodyParent;
    public TextMeshPro spotPopup;
    public State state;

    public enum State
    {
        Idle, 
        Spot, // brief moment before following
        Chase, 
    }



    private void Awake()
    {
        targetPlayer = GameObject.Find("Player");
        state = State.Idle;
        spotPopup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            case State.Idle:
                Vector2 playerDist = PlayerDistance();
                if (playerDist.magnitude < chaseDistance)
                {
                    state = State.Spot;
                }
                break;
            case State.Spot:
                StartCoroutine(Spot());
                break;
            case State.Chase:
                Chase();
                break;
        }

    }

    IEnumerator Spot()
    {
        spotPopup.enabled = true;
        yield return new WaitForSeconds(spotTime);
        spotPopup.enabled = false;
        state = State.Chase;
        gameObject.GetComponent<EnemyStats>().activeHealthbar(true);
    }
    void Chase()
    {
        Vector2 playerDist = PlayerDistance();

        transform.Translate(moveSpeed * Time.deltaTime * (playerDist.x < 0 ? 1 : -1), 0f, 0f);

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
