using UnityEngine;
using System.Collections;

public class LevelDoor : MonoBehaviour
{

    public Transform destPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                col.gameObject.transform.position = destPos.position;
            }
        }



    }
}

