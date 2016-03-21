using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

    public float disX;
    public float disY;
    public float disZ;

    public float cameraOrthSize;

    public Transform playerTrans;
    Vector3 playerPos;

    public bool isLeftSide;
    public bool isRightSide;

    Vector3 camPos;
    Camera mainCam;

    // Use this for initialization
    void Start()
    {
        mainCam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        
        mainCam.orthographicSize = cameraOrthSize;

        playerPos = playerTrans.transform.position;


        if (isLeftSide)
        {
            playerPos = new Vector3(playerPos.x + disX, playerPos.y + disY, disZ);
        }
        else if (isRightSide)
        {
            playerPos = new Vector3(playerPos.x - disX, playerPos.y + disY, disZ);
        }

        mainCam.transform.position = playerPos;

    }

   
}