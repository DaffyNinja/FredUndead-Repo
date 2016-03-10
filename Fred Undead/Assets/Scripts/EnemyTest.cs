using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTest : MonoBehaviour {

    public int enemyHealth;
    public Slider enemyHealthBar;

    public int damageCount;


	// Use this for initialization
	void Start ()
    {
        enemyHealthBar.maxValue = enemyHealth;
	}

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            print("Dead");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hand")
        {
            print("Hit");

            enemyHealth -= damageCount;
        }
    }
}
