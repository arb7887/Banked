using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform player;
    public float turnSpeed = 4f;
    [Header("Layers to be recognised in Camera Collision")]
    public LayerMask CameraOcclusion;

    private Vector3 offset;
    private Vector3 newPosition;
    Vector3 target;
    private float xRotation, yRotation;
    private float smoothingCoeff = 4.0f;
    void Start () {
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
        offset = new Vector3(0, 3, -8);
    }
	
	void LateUpdate () {
        xRotation += Input.GetAxis("Mouse X") * turnSpeed;
        yRotation -= Input.GetAxis("Mouse Y") * turnSpeed;

        yRotation = ClampAngle(yRotation, -70, 55);

        Quaternion rotation = Quaternion.Euler(yRotation, xRotation, 0);

        /*
        offset = (Quaternion.AngleAxis(xRotation, Vector3.up)
            * Quaternion.AngleAxis(yRotation, Vector3.left))
            * offset;

        transform.position = player.position + offset;
        */
        target = player.position;
        newPosition = player.position + rotation * offset;

        collisionOffset(ref target);
        //transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
        transform.position = newPosition;
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
    void collisionOffset(ref Vector3 target)
    {
        RaycastHit wallHit = new RaycastHit();
        if(Physics.Linecast(target, newPosition, out wallHit, CameraOcclusion))
        {
            newPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, newPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
    }
}
