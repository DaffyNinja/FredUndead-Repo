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

            print("Damage");

            col.gameObject.GetComponent<EnemyTest>().enemyHealth -= baseBat.damgeToDeal;

        }

    }
}
