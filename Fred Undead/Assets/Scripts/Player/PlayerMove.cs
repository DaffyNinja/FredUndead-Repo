using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour
{


    public float speed;
    // public float sprintSpeed;
    public float jumpHeight;

    float startSpeed;


    public float flipMove;
    public bool isFacingRight;
    [Space(5)]
    public float jumpRayLength;
    public bool canJump;
    public bool isJumping;
    public LayerMask groundLayer;

    // public bool passedLayer;

    public Transform checkPointPos;

    [Header("Decay")]
    public int healthPoints;

    public bool isCrawling;
    public bool isZombie;
    public bool isGhoul;
    public bool isUndying;

    public Sprite crawlerSpr;
    public Sprite zombieSpr;
    public Sprite undyingSpr;

    SpriteRenderer sprRnd;
    //public Slider decayBar;

    [Space(5)]
    public CameraMove camMove;

    private Animator playerAnimator;
    private Rigidbody2D rig2D;

    [Space(5)]
    public Transform passedTrans;
    public bool passedLayer;

    // Use this for initialization
    void Start()
    {
        // playerAnimator = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
        startSpeed = speed;

        sprRnd = GetComponent<SpriteRenderer>();

        isUndying = true;




    }

    // Update is called once per frame
    void Update()
    {
        Controls();

        Health();

        //if (passedLayer)
        //{
        //    Physics2D.IgnoreCollision(passedTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        //}
        //else if (!passedLayer)
        //{
        //    Physics2D.IgnoreCollision(passedTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        //}

        //decayBar.value = healthPoints;

    }

    void Controls()
    {
        // PC                                                        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            Vector2 moveQauntity = new Vector2(speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);


            flipMove = 1;


            // playerAnimator.SetBool("isRunning", true);


        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {


            Vector2 moveQauntity = new Vector2(-speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);


            flipMove = -1;

            //playerAnimator.SetBool("isRunning", true);


        }





        // Xbox Controller
        if (Input.GetAxis("Horizontal") >= 1)
        {
            // print("Right");

            Vector2 moveQauntity = new Vector2(speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);


            flipMove = 1;


            // playerAnimator.SetBool("isRunning", true);

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // print("Left");


            Vector2 moveQauntity = new Vector2(-speed, 0);
            rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);


            flipMove = -1;

            // playerAnimator.SetBool("isRunning", true);

        }



        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, jumpRayLength, groundLayer);  //PlayerMask and rayLength are public variables that need to be set 
                                                                                                             // Debug.DrawRay(transform.position, Vector2.left, Color.red, jumpRayLength);

        if (hit)
        {
            // print("Ray");

            //print(hit.ToString());

            if (Input.GetButtonDown("Jump"))
            {


                rig2D.velocity = Vector2.up * jumpHeight;

                // playerAnimator.SetBool("isJumping", true);

                isJumping = true;


            }
            else
            {
                isJumping = false;
            }

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

    void Health()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sprRnd.sprite = undyingSpr;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sprRnd.sprite = zombieSpr;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sprRnd.sprite = crawlerSpr;

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
        if (col.gameObject.tag == "Bullet")
        {
            // print("Hit");
        }

        //if (col.collider.gameObject.layer == LayerMask.NameToLayer("Plat"))
        //{

        //    passedTrans = col.transform;

        //    passedLayer = true;

        //}



    }

    //void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.collider.gameObject.layer == LayerMask.NameToLayer("Plat"))
    //    {
    //        passedTrans = null;

    //        passedLayer = false;

    //       // Physics2D.IgnoreCollision(col.collider.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);


    //    }

    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Death")
        {
            transform.position = checkPointPos.position;
        }

        if (col.gameObject.tag == "LeftSide")
        {
           // print("Left");

            camMove.isLeftSide = true;
            camMove.isRightSide = false;

        }
        else if (col.gameObject.tag == "RightSide")
        {
            //print("Right");

            camMove.isRightSide = true;
            camMove.isLeftSide = false;
        }

    }



}
