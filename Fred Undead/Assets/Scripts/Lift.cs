using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour
{

    public float speed;
    public Transform tStart;
    public Transform tEnd;

    public bool startMoving;

    private bool bIsMovingToEnd = true;
    private Vector3 vStart;
    private Vector3 vEnd;
    private float fMoveTimer = 0f;

    void Start()
    {
        vStart = tStart.position;
        vEnd = tEnd.position;
        transform.position = vStart;
    }

    void Update()
    {
        // If the player goes onto the lift, start moving
        if (startMoving)
        {

            fMoveTimer = Mathf.Clamp(fMoveTimer, 0, 1);

            if (bIsMovingToEnd)
            {
                fMoveTimer += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(vStart,
                                                  vEnd,
                                                  fMoveTimer);

                if (fMoveTimer >= 1f)
                {
                    startMoving = false;
                    // bIsMovingToEnd = false;
                }
            }
            //else
            //{
            //    fMoveTimer -= speed * Time.deltaTime;
            //    transform.position = Vector3.Lerp(vStart,
            //                                      vEnd,
            //                                      fMoveTimer);
            //    if (fMoveTimer <= 0f)
            //    {

            //        bIsMovingToEnd = true;

            //    }
            //}
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            startMoving = true;

            //Parents the player to the platform so they both move
            col.transform.parent = gameObject.transform;


        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            startMoving = false;

            //The player is no longer a child of the platform
            col.transform.parent = null;

            //if (col.gameObject.GetComponent<PlayerMove>().flipMove == 1)
            //{
            //    col.transform.localScale = new Vector3(1, 1, 1);
            //}
            //else
            //{
            //    col.transform.localScale = new Vector3(-1, 1, 1);
            //}
        }

    }
}