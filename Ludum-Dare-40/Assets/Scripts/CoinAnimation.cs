using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(0.0f, 0.0f, 30.0f * Time.deltaTime);
	}
}