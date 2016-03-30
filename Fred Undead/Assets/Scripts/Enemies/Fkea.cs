using UnityEngine;
using System.Collections;

public class Fkea : MonoBehaviour
{

    public float speed;

    public bool moveLeft;
    public bool moveRight;

    public float rayLength;
    public LayerMask wallLayer;

    BoxCollider2D ownCollider;
    //CircleCollider2D detectionCol;
    Rigidbody2D rig2D;

    // Use this for initialization
    void Start()
    {
        ownCollider = GetComponent<BoxCollider2D>();
       // detectionCol = transform.GetChild(0).GetComponent<CircleCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    void Movement()
    {
        // Raycast for difrent directions the enemy is facing
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLength, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayLength, wallLayer);

        if (hitLeft && hitLeft.collider != ownCollider)
        {
            // print("Hit Left");

            moveLeft = false;
            moveRight = true;

        }
        else if (hitRight && hitRight.collider != ownCollider)
        {
            // print("Hit Right");

            moveRight = false;
            moveLeft = true;

        }

        if (moveRight) // Moves in the right direction
        {
            Vector2 moveQauntity = new Vector2(speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

        }
        else if (moveLeft)  // Moves in the left direction
        {
            Vector2 moveQauntity = new Vector2(-speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            //print("Hit");

            GameObject playerObj = col.gameObject;

            moveRight = false;
            moveLeft = false;

            Vector2 moveQauntity = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
            rig2D.velocity = moveQauntity;

        }

    }
}
