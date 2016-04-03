using UnityEngine;
using System.Collections;

public class Lagona : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    [Header("Combat")]
    public GameObject squidObj;
    public Transform squidShootPos;

    public float shootTime;
    //  public float pauseTime;
    float timer;

    bool startShooting;

    [Header("Health")]
    public int healthPoints;


    GameObject hitHand;


    // Use this for initialization
    void Start()
    {
        hitHand = transform.GetChild(0).gameObject;
        squidShootPos = transform.GetChild(1);

        startShooting = true;

    }

    // Update is called once per frame
    void Update()
    {


        if (startShooting)
        {
            timer += Time.deltaTime;

            Instantiate(squidObj, squidShootPos.position, Quaternion.identity);

            startShooting = false;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= shootTime)
            {
                timer = 0;

                startShooting = true;
            }

        }



    }
}
