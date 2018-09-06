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
        offset = (Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up)
            * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * -turnSpeed, Vector3.right))
            * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);
	}
}
