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
    public Slider decayBar;

    [Space(5)]
    public CameraMove camMove;

    private Animator playerAnimator;
    private Rigidbody2D rig2D;

    [Space(5)]
    public Transform passedTrans;
    public bool passedLayer;

    PlayerCombat playerComb;

    // Use this for initialization
    void Start()
    {
        // playerAnimator = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
        startSpeed = speed;

        sprRnd = GetComponent<SpriteRenderer>();

        isUndying = true;

        playerComb = GetComponent<PlayerCombat>();

        decayBar.maxValue = healthPoints;
  

    }

    // Update is called once per frame
    void Update()
    {
        decayBar.value = healthPoints;

        Controls();

        Health();


    }

    void Controls()
    {
        // PC Controls
        // Moves the player when they hold down the movemet buttons                                                       
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



        // Xbox Controller Controls
        if (Input.GetAxis("Horizontal") >= 1)
        {

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


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, jumpRayLength, groundLayer);  // Raycast hit pointing down to indicate that i am on the ground

        if (hit)
        {
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

        // Which the way the plauer chracter flips
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
        // The many diffrent states the player changes into
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

    // Flips the player object
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


    }

    // If the player is inside the gun trigger area, they can pick it up 
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Gun")
        {
            //print("Gun");

            if (Input.GetKey(KeyCode.C))
            {
                print("Pickup");

                playerComb.pickedUpWeaponObj = col.gameObject;
                playerComb.hasWeapon = true;
            }
        }

    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Death")
        {
            transform.position = checkPointPos.position;
        }

        if (col.gameObject.tag == "Enemy Hand")
        {
            print("Been Hit");

            healthPoints -= 5;

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
