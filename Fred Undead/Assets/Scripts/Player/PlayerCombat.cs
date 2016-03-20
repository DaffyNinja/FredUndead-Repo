using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public int normalDamage;
    public GameObject hitHand;

    public float hitWaitTime;

    public bool canHit;

    float timer;


    // Use this for initialization
    void Start()
    {
        hitHand = transform.GetChild(0).gameObject;

        hitHand.SetActive(false);

        canHit = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (canHit)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                hitHand.SetActive(true);
                

            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                hitHand.SetActive(false);
                canHit = false;

            }
        }
        else if (!canHit)
        {
            timer += Time.deltaTime;

            if (timer >= hitWaitTime)
            {

                canHit = true;
                timer = 0;
            }
        }




    }


}
