using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    
    public float moveSpeed = 3.0f;
    public float jumpHeight = 7.0f;
    public float groundDist;
    public LayerMask floorLayer;
    public LayerMask dropDownLayer;
    public float dropDownTime = 0.4f;

    public GameObject armParent;
    public GameObject bodyParent;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        groundDist = GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidBody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.S) && OnTopLevel())
        {
            //Very clunky, review later
            StartCoroutine(DropDown());
        }
    }

    IEnumerator DropDown()
    {
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(dropDownTime);
        boxCollider.isTrigger = false;

    }

    private void FixedUpdate()
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

    bool IsGrounded()
    {
        //if falling or already jumping
        if (rigidBody.velocity.y != 0) return false;
        
        //Shoots a raycast to the ground 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDist+0.2f, floorLayer);
        
        //Returns true if the raycast hit an object with floorLayer Layer
        return hit.collider != null;
    }

    bool OnTopLevel()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDist + 0.2f, dropDownLayer );
        //Returns true if the raycast hit an object with floorLayer Layer
        return hit.collider != null;
    }
}
