using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour {
    public bool shouldAnimate = true;
    // Update is called once per frame
	void Update () {
        //Quaternion tov3Up = Quaternion.FromToRotation(transform.up, Vector3.up);
        if(shouldAnimate) gameObject.transform.Rotate(0.0f, 0.0f, 40.0f * Time.deltaTime);
	}
}