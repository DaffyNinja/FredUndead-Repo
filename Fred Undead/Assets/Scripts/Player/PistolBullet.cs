using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour
{


    public float lifeTime;
    float timer;

    public bool created;

                        

    // Use this for initialization
    void Start()
    { 

        created = true;


    }

    // Update is called once per frame
    void Update()
    {

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

