using UnityEngine;
using System.Collections;

public class BaseBallBat : MonoBehaviour
{

    public int damgeToDeal;

    public int useNum;

    public bool canUse;

    public bool dealDamge;

    public Transform baseBallHitTrig;

    PolygonCollider2D polyCol;

    // Use this for initialization
    void Start()
    {
        polyCol = GetComponent<PolygonCollider2D>();

        baseBallHitTrig = transform.GetChild(0);

        // polyCol.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canUse)
        {
            //polyCol.enabled = true;

            if (Input.GetKeyDown(KeyCode.X))
            {
                transform.eulerAngles = new Vector3(0, 0, -35);

                dealDamge = true;
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);

                dealDamge = false;
            }
        }

        if (useNum <= 0)
        {
            canUse = false;

            dealDamge = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (dealDamge)
        {
            if (col.gameObject.name == "Tropper")
            {
                useNum--;

                col.gameObject.GetComponent<Troop>().troopHealthPoints -= damgeToDeal;

            }
            if (col.gameObject.name == "Enemy")
            {
                print("Damage");

                

                col.gameObject.GetComponent<EnemyTest>().enemyHealth -= damgeToDeal;


            }
        }
    }
}


