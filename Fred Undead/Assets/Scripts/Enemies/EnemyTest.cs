using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTest : MonoBehaviour
{

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
    public bool isInCombat;


    [Header("Combat")]
    public int normalDamage;
    GameObject hitHand;
    public float hitWaitTime;
    public bool canHit;
    float timer;


    public float sightLength;
    public LayerMask playerLayer;

    bool startFighting;

    RaycastHit2D hit;

    [Space(5)]
    public int enemyHealth;
    public Slider enemyHealthBar;

    public int damageCount;

    Rigidbody2D rig2D;

    BoxCollider2D ownCollider;
    BoxCollider2D handCol;


    // Use this for initialization
    void Start()
    {
        hitHand = transform.GetChild(0).gameObject;
        handCol = hitHand.GetComponent<BoxCollider2D>();

        ownCollider = GetComponent<BoxCollider2D>();

        enemyHealthBar.maxValue = enemyHealth;

        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Combat();

        enemyHealthBar.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            //print("Dead");

            Destroy(gameObject);


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

    void Combat()
    {

        RaycastHit2D comHitLeft = Physics2D.Raycast(transform.position, Vector2.left, sightLength, playerLayer);
        RaycastHit2D comHitRight = Physics2D.Raycast(transform.position, Vector2.right, sightLength, playerLayer);

        // If the enemy sees the player go into comabat mode
        if (comHitLeft && comHitLeft.collider != ownCollider || comHitRight && comHitRight.collider != ownCollider)
        {

            //print("Player");

            isInCombat = true;
            isPatrolling = false;



        }
        else
        {
            isPatrolling = true;
            isInCombat = false;


            //canHit = false;

        }

        // How and when the shoots
        if (isInCombat)
        {
            if (canHit)
            {
                hitHand.SetActive(true);
                timer += Time.deltaTime;

                if (timer >= hitWaitTime)
                {
                    hitHand.SetActive(false);

                    if (timer >= hitWaitTime + hitWaitTime)
                    {
                        timer = 0;
                    }
                }

            }

            // print(timer.ToString());

        }


    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hand")
        {
            //  print("Hit");

            enemyHealth -= damageCount;
        }

        if (col.gameObject.tag == "Pistol Bullet")
        {
           // print("Hit");

            enemyHealth -= 10;

        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        
    }
}
