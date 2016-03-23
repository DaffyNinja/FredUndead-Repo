using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponPickup : MonoBehaviour
{


    public List<GameObject> weaponsList = new List<GameObject>();

    public int weaponNum;

    public GameObject weaponObj;


    // Use this for initialization
    void Start()
    {
        //weaponObj = weaponsList[weaponNum];

        weaponObj = Instantiate(weaponsList[weaponNum], transform.position, Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {


    }

    // If the player is inside the gun trigger area, they can pick it up 
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {

                col.gameObject.GetComponent<PlayerCombat>().pickedUpWeaponObj = weaponObj;
                col.gameObject.GetComponent<PlayerCombat>().hasWeapon = true;

                weaponObj.transform.parent = col.gameObject.transform;
                weaponObj.GetComponent<SpriteRenderer>().sortingOrder = 2;

                switch (weaponObj.tag)
                {
                    case "Gun":
                        weaponObj.GetComponent<Pistol>().canUse = true;
                        break;
                    case "BaseBall Bat":
                        weaponObj.GetComponent<BaseBallBat>().canUse = true;
                        
                        break;
                    default:
                        print("Error");
                        break;
                }

                Destroy(gameObject);
            }
        }


    }
}
