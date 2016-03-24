using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour
{
    public float bulletSpeed;

    public float lifeTime;
    float timer;

    public Transform shootPos;
    public Rigidbody2D bullet;

    public PlayerMove playerObj;

    bool created;


    public bool canUse;


    void Start()
    {
        shootPos = transform.GetChild(0);

        created = true;
    }

    void Fire()
    {
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, shootPos.position, Quaternion.identity);

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

                Fire();
        }
    }
}

