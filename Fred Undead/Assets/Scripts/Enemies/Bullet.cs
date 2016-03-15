using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed;

    public float lifeTime;
    float timer;

    public bool created;

    public bool moveLeft;
    public bool moveRight;

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
        if (moveLeft)
        {
            //Vector2 moveQauntity = new Vector2(-speed, 0);
            //rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

            transform.Translate(speed, 0, 0);
        }
        else if (moveRight)
        {
            //Vector2 moveQauntity = new Vector2(speed, 0);
            //rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

            transform.Translate(-speed, 0, 0);
        }

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
