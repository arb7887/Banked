using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform player;
    public float turnSpeed = 4f;

    private Vector3 offset;
    private float xRotation, yRotation;
    void Start () {
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
        offset = transform.position;
    }
	
	void LateUpdate () {
        xRotation += Input.GetAxis("Mouse X") * turnSpeed;
        yRotation -= Input.GetAxis("Mouse Y") * turnSpeed;

        yRotation = ClampAngle(yRotation, -20, 55);

        Quaternion rotation = Quaternion.Euler(yRotation, xRotation, 0);

        /*
        offset = (Quaternion.AngleAxis(xRotation, Vector3.up)
            * Quaternion.AngleAxis(yRotation, Vector3.left))
            * offset;

        transform.position = player.position + offset;
        */

        transform.position = player.position + rotation * offset;

        transform.LookAt(player.position);
    }
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
