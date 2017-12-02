using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private float h_acceleration = 10f;
	// Update is called once per frame
	void Update () {
        Vector3 velocity = new Vector3(0f, 0f);
        if (Input.GetKey(KeyCode.W)) velocity.z += h_acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) velocity.z -= h_acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) velocity.x -= h_acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) velocity.x += h_acceleration * Time.deltaTime;
        Mathf.Clamp(velocity.x, 0, 10f);
        Mathf.Clamp(velocity.z, 0, 10f);
        gameObject.transform.Translate(velocity);
    }
}
