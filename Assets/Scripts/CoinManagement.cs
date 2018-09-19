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
        if (Input.GetKeyDown(KeyCode.Mouse1) && amountOfCoins > 0)
        {
            GameObject newcoin = Instantiate(coin, transform.position - (transform.forward.normalized * 2.0f), Quaternion.identity);
            amountOfCoins--;
            newcoin.GetComponent<Rigidbody>().useGravity = true;
            newcoin.GetComponent<CoinAnimation>().shouldAnimate = false;
            Vector3 velocity = -transform.forward.normalized * 2.0f;
            newcoin.GetComponent<Rigidbody>().velocity = velocity;
            newcoin.GetComponent<Rigidbody>().angularVelocity = new Vector3(-4.0f, 0.0f, 0.0f);
        }
        scoreText.GetComponent<TextMeshProUGUI>().SetText(amountOfCoins.ToString());
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Coin")
        {
            c.gameObject.SetActive(false);
            Destroy(c.gameObject);
            amountOfCoins++;
        }
    }
}
