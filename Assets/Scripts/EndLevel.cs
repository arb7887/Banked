using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    public GameObject player;
    public int coinRequirement;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.name == "Player" && player.GetComponent<CoinManagement>().coinMultiplier >= coinRequirement)
        {
            Application.Quit();
        }
    }
}
