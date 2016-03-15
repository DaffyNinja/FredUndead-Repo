using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public int normalDamage;
    public GameObject hitHand;


    // Use this for initialization
    void Start()
    {
        hitHand = transform.GetChild(0).gameObject;

        hitHand.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hitHand.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            hitHand.SetActive(false);
        }




    }

    
}
