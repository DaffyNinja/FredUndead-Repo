using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pistol : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletDamage;
    public int ammoCount;




    public Transform shootPos;
    public Rigidbody2D bullet;

    Rigidbody2D bulletClone;

    public PlayerMove playerObj;


    public bool canUse;

    public PlayerCombat playerCom;


    void Start()
    {
        shootPos = transform.GetChild(0);
    }

    void Fire()
    {

        ammoCount -= 1;

        bulletClone = (Rigidbody2D)Instantiate(bullet, shootPos.position, Quaternion.identity);

        if (playerObj.isFacingRight)
        {
            bulletClone.velocity = transform.TransformDirection((Vector3.left * bulletSpeed * Time.deltaTime));
        }
        else
        {
            bulletClone.velocity = transform.TransformDirection((Vector3.right * bulletSpeed * Time.deltaTime));
        }

    }

    void Update()
    {



        if (canUse)
        {

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Fire();

            }
        }

        if (ammoCount <= 0)
        {
            playerCom.hasWeapon = false;
            canUse = false;

            Destroy(gameObject);
        }





        //print(timer.ToString());
    }
}

