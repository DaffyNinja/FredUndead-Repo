using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Troop : MonoBehaviour
{
    [Header("Movement and Health")]
    public float speed;
    public int troopHealthPoints;
    public Slider troopEnemyBar;


    [Header("Shoot and Find")]
    public GameObject bulletObj;
    public Transform bulletSpawn;
    [Space(5)]
    public float gunSightLength;
    public bool startShooting;

    public float shootTime;
    public float pauseShootTime;

    float shootTimer;
    float pauseShootTimer;
    [Space(5)]
    public float rayLength;
    public LayerMask wallLayer;

    public bool moveLeft;
    public bool moveRight;

    public bool canHitLeft;
    public bool canHitRight;

    public bool isFacingRight;
    float flipMove;

    //Combat States
    [Header("Combat States")]
    public bool isPatrolling;
    public bool isInCombat;


    Bullet bulletCS;

    RaycastHit2D hit;

    Rigidbody2D rig2D;
    Animator anim;

    [Space(5)]
    public PlayerCombat playCom;

    // Use this for initialization
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        bulletSpawn = transform.GetChild(0);

        troopEnemyBar.maxValue = troopHealthPoints;

        bulletCS = bulletObj.GetComponent<Bullet>();



    }

    // Update is called once per frame
    void Update()
    {
        troopEnemyBar.value = troopHealthPoints;

        Move();

        Combat();

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


        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLength, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayLength, wallLayer);

        if (canHitLeft)
        {
            if (hitLeft)
            {
                //print("Left");

                moveLeft = false;
                moveRight = true;

                canHitLeft = false;
                canHitRight = true;

                Flip();

            }
        }

        if (canHitRight)
        {
            if (hitRight)
            {
                // print("Right");

                moveRight = false;
                moveLeft = true;


                canHitLeft = true;
                canHitRight = false;

                Flip();

            }
        }

        if (isPatrolling)
        {
            if (moveRight)
            {
                Vector2 moveQauntity = new Vector2(speed, 0);
                rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

            }
            else if (moveLeft)
            {
                Vector2 moveQauntity = new Vector2(-speed, 0);
                rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

            }

        }

    }

    void Combat()
    {

        if (!isFacingRight)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, gunSightLength);

            bulletCS.moveLeft = true;
            bulletCS.moveRight = false;

            // Debug.DrawRay(transform.position, Vector2.left, Color.green, hit.distance);
        }
        else
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, gunSightLength);
            // Debug.DrawRay(transform.position, Vector2.right, Color.green,hit.distance);

            bulletCS.moveRight = true;
            bulletCS.moveLeft = false;

        }


        //if (hit)
        //{
        //    if (hit.collider.tag == "Player")
        //    {
        //        //print("Player");

        //        isInCombat = true;
        //        isPatrolling = false;

        //    }

        //}
        //else
        //{
        //    isPatrolling = true;
        //    isInCombat = false;
        //}


        if (isInCombat)
        {
           pauseShootTimer += Time.deltaTime;

            //print("Shoot " + shootTimer.ToString());

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

              //  print("Pause " + pauseShootTimer.ToString());

                if (shootTimer>= shootTime)
                {
                    shootTimer = 0;

                    pauseShootTimer = 0;

                    startShooting = false;

                }

                anim.SetBool("isShooting", true);
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
            troopHealthPoints -= playCom.normalDamage;

        }
    }


}
