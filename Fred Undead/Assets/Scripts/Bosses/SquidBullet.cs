using UnityEngine;
using System.Collections;

public class SquidBullet : MonoBehaviour
{

    public int squidDamage;
    public float speed;

    Rigidbody2D rig2D;

    // Use this for initialization
    void Start()
    {

        rig2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveQauntity = new Vector2(-speed, 0);
        rig2D.velocity = new Vector2(moveQauntity.x, rig2D.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            // print("Player");

            col.gameObject.GetComponent<PlayerMove>().healthPoints -= squidDamage;

        }

        if (col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }

       
    }
}
