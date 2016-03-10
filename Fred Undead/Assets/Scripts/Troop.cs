using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {

    public GameObject bulletObj;
    public Transform bulletSpawn;
    [Space(5)]
    public float sightLength;
    public bool startShooting;

    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();


        
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, sightLength);

        if (hit)
        {
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


    }
}
