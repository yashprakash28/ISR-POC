using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewChange : MonoBehaviour
{
    float moveUD, moveLR, rotateFB, rotateLR, moveFB;

    // Update is called once per frame
    void Update()
    {
        moveLR = Input.GetAxis("Horizontal");
        moveUD = Input.GetAxis("Vertical");
        moveFB = Input.GetAxis("ZAxisMovement");
        rotateLR = Input.GetAxis("RotateHorizontal");
        rotateFB = Input.GetAxis("RotateVertical");

// Debug.Log(moveFB);
        // Debug.Log("jshdbvcj   " + rotateFB);

        if (moveLR != 0)    translateLR();
        if (moveUD != 0)    translateUD();
        if (moveFB != 0)    translateFB();
        if (rotateFB != 0)    rotFB();
        if (rotateLR != 0)    rotLR();
    }

    void translateLR()
    {
        Debug.Log("moving horizontaly");
        transform.position += transform.right * moveLR * Time.deltaTime;
    }

    void translateUD()
    {
        Debug.Log("moving vertically");
        transform.position += transform.up * moveUD * Time.deltaTime;
    }

    void translateFB()
    {
        Debug.Log("moving forward");
        transform.position += transform.forward * moveFB * Time.deltaTime;
    }

    void rotLR()
    {
        Debug.Log("rotLR");
        transform.Rotate(new Vector3(0f, rotateLR*Time.deltaTime, 0f));
        // transform.eulerAngles += new Vector3(0f, rotateFB*Time.deltaTime, 0f);

    }

    void rotFB()
    {
        Debug.Log("rotFB");
        transform.Rotate(new Vector3(rotateFB*Time.deltaTime, 0f, 0f));
        // transform.eulerAngles += new Vector3(rotateLR*Time.deltaTime, 0f, 0f);

    }

}
