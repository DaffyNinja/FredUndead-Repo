using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float disX;
    public float disY;
    public float disZ;

    public float cameraOrthSize;

    public Transform playerTrans;
    Vector3 playerPos;

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

        playerPos = new Vector3(playerPos.x + disX, playerPos.y + disY, disZ);

        mainCam.transform.position = playerPos;

    }
}