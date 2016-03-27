using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{

    public int normalDamage;
    public GameObject hitHand;

    public float hitWaitTime;

    public bool canHit;

    float timer;

    [Header("Weapons")]
    public GameObject pickedUpWeaponObj;
    public bool hasWeapon;

    bool canDrop;


    PlayerMove playerMoveCS;

    // Use this for initialization
    void Start()
    {
        hitHand = transform.GetChild(0).gameObject;

        hitHand.SetActive(false);

        canHit = true;

        playerMoveCS = GetComponent<PlayerMove>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canHit)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Joystick X"))
            {
                hitHand.SetActive(true);


            }
            else if (Input.GetKeyUp(KeyCode.Z) || Input.GetButtonUp("Joystick X"))
            {
                hitHand.SetActive(false);
                canHit = false;

            }
        }
        else if (!canHit)
        {
            timer += Time.deltaTime;

            if (timer >= hitWaitTime)
            {

                canHit = true;
                timer = 0;
            }
        }

        if (hasWeapon)
        {
            canHit = false;

            if (!playerMoveCS.isFacingRight)
            {
                pickedUpWeaponObj.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                pickedUpWeaponObj.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            switch (pickedUpWeaponObj.transform.tag)
            {
                case "Gun":
                    pickedUpWeaponObj.GetComponent<Pistol>().playerCom = GetComponent<PlayerCombat>();
                    pickedUpWeaponObj.GetComponent<Pistol>().canUse = true;
                    pickedUpWeaponObj.GetComponent<Pistol>().playerObj = playerMoveCS;

                   


                    break;
                case "BaseBall Bat":
                    pickedUpWeaponObj.GetComponent<BaseBallBat>().playerCom = GetComponent<PlayerCombat>();
                    pickedUpWeaponObj.GetComponent<BaseBallBat>().canUse = true;

                    canDrop = true;
   

                    break;
                default:
                    print("Error");
                    break;
            }

            

        }
        else
        {
            canHit = true;
        }

        if (canDrop)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                print("Drop");
            }
        }


    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            // print("Bullet Hit");

            playerMoveCS.healthPoints -= 5;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Hand")
        {
            //print("Been Hit");

            playerMoveCS.healthPoints -= 5;

        }
    }
}
