using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour
{

    public float speed;
    [Header("Shoot")]
    public GameObject bulletObj;
    public Transform bulletSpawn;
    [Space(5)]
    public float sightLength;
    public bool startShooting;

    bool isFacingRight;
    float flipMove;


    Rigidbody2D rig2D;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Move();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, sightLength);

        Debug.DrawRay(transform.position, hit.transform.position, Color.red);

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

            if (hit.collider.tag == "Wall")
            {
                print("Wall");

                Flip();
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
        Vector2 moveQauntity = new Vector2(-speed, 0);
        rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

        flipMove = 1;

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
