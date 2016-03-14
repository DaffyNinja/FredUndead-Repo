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
    [Space(5)]
    public float rayLength;
    public LayerMask wallLayer;

    public bool moveLeft;
    public bool moveRight;

    public bool canHitLeft;
    public bool canHitRight;



    bool isFacingRight;
    float flipMove;

    RaycastHit2D hit;

    Rigidbody2D rig2D;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        bulletSpawn = transform.GetChild(0);

        troopEnemyBar.maxValue = troopHealthPoints;
       


    }

    // Update is called once per frame
    void Update()
    {
        troopEnemyBar.value = troopHealthPoints;

        Move();


        if (isFacingRight == false)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x + 5, transform.position.y), Vector2.left, gunSightLength);

        }
        else if (isFacingRight == true)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x - 5, transform.position.y), Vector2.right, gunSightLength);
        }

        if (hit)
        {
            //print("Hit");

            if (hit.collider.tag == "Player")
            {
                print("Player");

                startShooting = true;

                anim.SetBool("isShooting", true);

                //Shoot

                Instantiate(bulletObj, bulletSpawn.position, Quaternion.identity);
            }

        }
        else
        {
            startShooting = false;

            anim.SetBool("isShooting", false);
        }



        if (flipMove > 0 && isFacingRight)
        {
            Flip();
        }
        else if (flipMove < 0 && !isFacingRight)
        {
            Flip();
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

            }
        }

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
            troopHealthPoints -= 1;
              
        }
    }


}
