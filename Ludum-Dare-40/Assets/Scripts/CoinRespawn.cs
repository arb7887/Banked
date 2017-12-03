using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRespawn : MonoBehaviour {

    private GameObject ATM;
    private void Awake()
    {
        ATM = GameObject.FindGameObjectWithTag("ATM");
        if(ATM == null)
        {
            Debug.Log("Could not Find ATM Object");
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "DeathTrigger")
        {
            transform.position = ATM.transform.position + ATM.transform.forward * 0.5f;
            Vector3 velocity = ATM.transform.forward * 2.0f;
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
