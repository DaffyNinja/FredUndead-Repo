using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTest : MonoBehaviour {

    [Header("Movement")]
    public float speed;

    public bool moveLeft;
    public bool moveRight;

    public bool canHitLeft;
    public bool canHitRight;

    public bool isFacingRight;
    float flipMove;
    [Space(5)]
    public float rayLength;
    public LayerMask wallLayer;

    public bool isPatrolling;

  

    [Space(5)]
    public int enemyHealth;
    public Slider enemyHealthBar;

    public int damageCount;

    Rigidbody2D rig2D;

    BoxCollider2D ownCollider;


    // Use this for initialization
    void Start ()
    {
        ownCollider = GetComponent<BoxCollider2D>();

        enemyHealthBar.maxValue = enemyHealth;

        rig2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        Move();

        enemyHealthBar.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            print("Dead");
        }


    }

    void Move()
    {
        // Raycast for difrent directions the enemy is facing
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLength, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayLength, wallLayer);

        // If the enemy has a wall on the left, move right  
        if (canHitLeft)
        {
            if (hitLeft && hitLeft.collider != ownCollider)
            {
                //print("Left");

                moveLeft = false;
                moveRight = true;

                canHitLeft = false;
                canHitRight = true;

                Flip();

            }
        }
        else if (canHitRight)  // Else if the enemy has a wall on the right, move left
        {
            if (hitRight && hitRight.collider != ownCollider)
            {
                // print("Right");

                moveRight = false;
                moveLeft = true;


                canHitLeft = true;
                canHitRight = false;

                Flip();

            }
        }

        // If the enemy is in a patrolling state it moves its patrolling movement
        if (isPatrolling)
        {
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

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hand")
        {
            print("Hit");

            enemyHealth -= damageCount;
        }
    }
}
