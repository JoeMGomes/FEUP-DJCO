using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;

    public float moveSpeed = 3.0f;
    public float jumpHeight = 7.0f;
    public LayerMask floorLayer;
    public LayerMask dropDownLayer;
    public float dropDownTime = 0.4f;

    public GameObject armParent;
    public GameObject bodyParent;
    public Transform feetPosition;

    public bool onWater = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.gameIsPaused)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.Space) && GroundCollision() != null)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Collider2D col = OnTopLevel();
                if (col != null)
                {
                    StartCoroutine(DropDown(col));
                }
            }
        }
    }

    private void Jump()
    {
        rigidBody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerJump);
    }

    IEnumerator DropDown(Collider2D col)
    {
        PlatformEffector2D plt = col.gameObject.GetComponent<PlatformEffector2D>();
        plt.rotationalOffset = 180;
        while (GroundCollision() == null || GroundCollision().gameObject == col.gameObject)
            yield return 0; //wait one frame
        plt.rotationalOffset = 0;

    }

    private void FixedUpdate()
    {
        if (!LevelManager.gameIsPaused)
        {
            Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - armParent.transform.position;

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

    public void Flinch(Vector3 hitpos)
    {
        float x = 5;
        if (hitpos.x > transform.position.x)
            x = -x;

        rigidBody.AddForce(new Vector2(x, 5), ForceMode2D.Impulse);
    }

    Collider2D GroundCollision()
    {
        //if falling or already jumping
        if (rigidBody.velocity.y != 0 || onWater) return null;

        //Shoots a raycast to the ground 
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, 0.2f, floorLayer);
        //Returns true if the raycast hit an object with floorLayer Layer
        return hit.collider;
    }

    Collider2D OnTopLevel()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, 0.2f, dropDownLayer);
        //Returns true if the raycast hit an object with floorLayer Layer
        return hit.collider;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("LevelBorder"))
        {
            Flinch(col.transform.position);
        }
    }
}