using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {

    public float sightLength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, sightLength);

        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                print("Player");
            }
        }
        

    }
}
