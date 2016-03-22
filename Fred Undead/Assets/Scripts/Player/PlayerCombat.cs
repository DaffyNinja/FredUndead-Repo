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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                hitHand.SetActive(true);
                

            }
            else if (Input.GetKeyUp(KeyCode.Z))
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
            pickedUpWeaponObj.transform.parent = gameObject.transform;
            pickedUpWeaponObj.GetComponent<SpriteRenderer>().sortingOrder = 2;

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
                    pickedUpWeaponObj.GetComponent<Pistol>().canUse = true;
                    break;
                default:
                    print("Error");
                    break;
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

        if (col.gameObject.tag == "Enemy Hand")
        {
            //print("Been Hit");

           
            playerMoveCS.healthPoints -= 5;

        }

    }


}
