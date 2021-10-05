using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //[SerializeField]
    //private Transform playerTr;
    //[SerializeField]
    //private Transform cameraArm;

    public GameObject camY;
    public GameObject camZ;

    void Start()
    {
        
    }

    void Update()
    {
        //LookAround();
        ChangeCam();
    }

    void ChangeCam()
    {
        if(Input.GetKey(KeyCode.K))
        {
            camY.SetActive(true);
        }
        else
        {
            camY.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            camZ.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camZ.SetActive(true);
        }
    }

    //private void LookAround()
    //{
    //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //    Vector3 camAngle = cameraArm.rotation.eulerAngles;
    //    cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    //}
}
