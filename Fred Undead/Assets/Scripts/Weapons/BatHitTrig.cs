using UnityEngine;
using System.Collections;

public class BatHitTrig : MonoBehaviour {

    BaseBallBat baseBat;

    // Use this for initialization
    void Start()
    {
        baseBat = GetComponentInParent<BaseBallBat>();

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<EnemyTest>() != null)
            {
                print("Enemy");

                col.gameObject.GetComponent<EnemyTest>().enemyHealth -= baseBat.damgeToDeal;

                baseBat.useNum -= 1;
            }
            else if (col.gameObject.GetComponent<Troop>() != null)
            {
                print("Troop");

                col.gameObject.GetComponent<Troop>().troopHealthPoints -= baseBat.damgeToDeal;

                baseBat.useNum -= 1;

            }

        }

    }
}
