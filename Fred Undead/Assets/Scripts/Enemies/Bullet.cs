using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed;

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
        transform.Translate(speed, 0, 0);

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
