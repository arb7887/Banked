using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform player;
    public float turnSpeed = 4f;

    private Vector3 offset;

    void Start () {
        offset = transform.position;
    }
	
	void LateUpdate () {
        float xRotation = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotation = Input.GetAxis("Mouse Y") * -turnSpeed;

        offset = (Quaternion.AngleAxis(xRotation, Vector3.up)
            * Quaternion.AngleAxis(yRotation, Vector3.left))
            * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
