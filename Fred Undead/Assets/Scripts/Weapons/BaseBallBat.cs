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

    public PlayerCombat playerCom;

    // Use this for initialization
    void Start()
    {
        polyCol = GetComponent<PolygonCollider2D>();

        baseBallHitTrig = transform.GetChild(0);

        baseBallHitTrig.gameObject.SetActive(false);

        // polyCol.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canUse)
        {
            //polyCol.enabled = true;

            if (Input.GetKey(KeyCode.X))
            {
                // transform.eulerAngles = new Vector3(0, 0, -35);

                baseBallHitTrig.gameObject.SetActive(true);

                dealDamge = true;
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                // transform.eulerAngles = new Vector3(0, 0, 0);

                baseBallHitTrig.gameObject.SetActive(false);

                dealDamge = false;
            }
        }

        if (useNum <= 0)
        {

            playerCom.hasWeapon = false;

            canUse = false;

            dealDamge = false;

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (dealDamge)
        {
            if (col == baseBallHitTrig.GetComponent<Collider2D>())
            {
                print("Hit");

                useNum--;

                col.gameObject.GetComponent<Troop>().troopHealthPoints -= damgeToDeal;

            }

            if (col == baseBallHitTrig.GetComponent<BoxCollider2D>())
            {
                if (col.gameObject.tag == "Enemy")
                {
                    print("Damage");

                    col.gameObject.GetComponent<EnemyTest>().enemyHealth -= damgeToDeal;
                }
            }

            //if (baseBallHitTrig.GetComponent<Collider2D>().tag == "Enemy")
            //{
            //    print("Damage");



            //    col.gameObject.GetComponent<EnemyTest>().enemyHealth -= damgeToDeal;


            //}

        }
    }
}


