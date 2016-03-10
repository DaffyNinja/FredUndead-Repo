using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{


    public float speed;
    // public float sprintSpeed;
    public float jumpHeight;

    float startSpeed;


    float flipMove;
    public bool isFacingRight;
    [Space(5)]
    public float jumpRayLength;
    public bool canJump;
    public bool isJumping;
    public LayerMask groundLayer;

    public bool passedLayer;

    public Transform checkPointPos;


    private Animator playerAnimator;
    private Rigidbody2D rig2D;

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        rig2D = GetComponent<Rigidbody2D>();

        startSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {

        Controls();

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
        //else
        //{
        //    playerAnimator.SetBool("isRunning", false);
        //}




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
        //else
        //{
        //    playerAnimator.SetBool("isRunning", false);
        //}



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
        //else
        //{
        //    playerAnimator.SetBool("isJumping", false);

        //}


        if (flipMove > 0 && isFacingRight)
        {
            Flip();
        }
        else if (flipMove < 0 && !isFacingRight)
        {
            Flip();
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
        if (passedLayer == false)
        {
            if (col.collider.gameObject.layer == LayerMask.NameToLayer("Sec 2"))
            {
                passedLayer = true;

                print("Hit");

                Physics2D.IgnoreCollision(col.collider.GetComponent<Collider2D>(), GetComponent<Collider2D>());

               // passedLayer = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Death")
        {
            transform.position = checkPointPos.position;
        }
    }



}
