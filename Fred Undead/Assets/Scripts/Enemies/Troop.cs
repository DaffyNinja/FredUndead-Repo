using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Troop : MonoBehaviour
{
    [Header("Movement and Health")]
    public float speed;
    public int troopHealthPoints;
    public Slider troopEnemyBar;

    public bool moveLeft;
    public bool moveRight;

    public bool canHitLeft;
    public bool canHitRight;

    public bool isFacingRight;
    float flipMove;
    [Space(5)]
    public float rayLength;
    public LayerMask wallLayer;


    [Header("Shoot and Find")]
    public GameObject bulletObj;
    public Transform bulletSpawn;
    [Space(5)]
    public float gunSightLength;
    public bool startShooting;
    public bool canShoot;

    public LayerMask playerLayer;

    public float shootTime;
    public float pauseShootTime;

    float shootTimer;
    float pauseShootTimer;




    //Combat States
    [Header("Combat States")]
    public bool isPatrolling;
    public bool isInCombat;


    Bullet bulletCS;

    RaycastHit2D hit;

    Rigidbody2D rig2D;
    Animator anim;

    BoxCollider2D ownCollider;

    [Space(5)]
    public PlayerCombat playCom;

    [Header("Effects")]
    public ParticleSystem bloodEffect;

    // Use this for initialization
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        bulletSpawn = transform.GetChild(0);

        troopEnemyBar.maxValue = troopHealthPoints;

        bulletCS = bulletObj.GetComponent<Bullet>();

        ownCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        troopEnemyBar.value = troopHealthPoints;

        Move();

        if (canShoot)
        {
            Combat();
        }

        // Flips the sprite and enemy
        if (flipMove > 0 && isFacingRight)
        {
            Flip();
        }
        else if (flipMove < 0 && !isFacingRight)
        {
            Flip();
        }

        if (troopHealthPoints <= 0)
        {
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

    // Holds what the enemy needs to do associating to its combat state 
    void Combat()
    {

        RaycastHit2D comHitLeft = Physics2D.Raycast(transform.position, Vector2.left, gunSightLength, playerLayer);
        RaycastHit2D comHitRight = Physics2D.Raycast(transform.position, Vector2.right, gunSightLength, playerLayer);

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

            pauseShootTimer += Time.deltaTime;

            //Shoot
            if (pauseShootTimer >= pauseShootTime)
            {
                startShooting = true;
            }
            else
            {
                //startShooting = false;
                anim.SetBool("isShooting", false);
            }

            if (startShooting)
            {
                Instantiate(bulletObj, bulletSpawn.position, Quaternion.identity);

                shootTimer += Time.deltaTime;

                if (shootTimer >= shootTime)
                {
                    shootTimer = 0;

                    pauseShootTimer = 0;

                    startShooting = false;

                }

                anim.SetBool("isShooting", true);
            }

        }
    }

    // Flips the enemy objects
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

    // If then enemy has been hit by the player, it gets dealt damage
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hand")
        {
            troopHealthPoints -= playCom.normalDamage;

            Instantiate(bloodEffect, transform.position, Quaternion.identity);



        }

        if (col.gameObject.tag == "Pistol Bullet")
        {
            troopHealthPoints -= 10;

            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
    }


}
