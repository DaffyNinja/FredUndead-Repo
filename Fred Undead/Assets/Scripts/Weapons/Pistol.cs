using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour {

    public GameObject bulletObj;
    public Transform shotArea;

    public bool canUse;

	// Use this for initialization
	void Start ()
    {
        shotArea = transform.GetChild(0);
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (canUse)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                print("Fire");

                //Instantiate(bulletObj, shotArea.transform.position, Quaternion.identity);
            }
        }
	
	}
}
