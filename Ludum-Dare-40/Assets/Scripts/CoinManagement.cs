using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManagement : MonoBehaviour {
    public int amountOfCoins;
    public GameObject coin;
    public GameObject scoreText;
    void Awake()
    {
        amountOfCoins = 0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && amountOfCoins > 0)
        {
            GameObject newcoin = Instantiate(coin, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.0f), Quaternion.identity);
            amountOfCoins--;
            newcoin.GetComponent<Rigidbody>().useGravity = true;
            newcoin.GetComponent<CoinAnimation>().shouldAnimate = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), newcoin.GetComponent<Collider>());
            Vector3 velocity = Vector3.back * 2.0f;
            newcoin.GetComponent<Rigidbody>().velocity = velocity;
            newcoin.GetComponent<Rigidbody>().angularVelocity = new Vector3(-4.0f, 0.0f, 0.0f);
        }
        scoreText.GetComponent<TextMeshPro>().text = amountOfCoins.ToString();
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Coin")
        {
            c.gameObject.SetActive(false);
            amountOfCoins++;
        }
    }
}
