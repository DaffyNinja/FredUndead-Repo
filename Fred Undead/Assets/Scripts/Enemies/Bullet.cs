﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
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
