using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour
{

    public float speed;

    public float lifeTime;
    float timer;

    public bool created;


    Rigidbody2D rig2D;

    // Use this for initialization
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();

        created = true;


    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(transform.forward * speed * Time.deltaTime);

        // rig2D.velocity = transform.forward * Time.deltaTime;

        //  rig2D.velocity = new Vector2(speed * Time.deltaTime, 0);


        //  print(timer.ToString());


        if (created == true)
        {

            timer += Time.deltaTime;

            if (timer >= lifeTime)
            {

                created = false;
                timer = 0;
                Destroy(gameObject);

            }
        }

    }
}

