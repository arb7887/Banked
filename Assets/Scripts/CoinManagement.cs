using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManagement : MonoBehaviour {
    private enum CoinType
    {
        Normal,
        Negative,
        Light
    };
    private List<CoinType> availableCoinTypes;
    private CoinType currentCoinType;

    public int amountOfCoins;
    public GameObject coin;
    public GameObject scoreText;
    void Awake()
    {
        availableCoinTypes = new List<CoinType>();
        amountOfCoins = 0;
    }
    void Update()
    {
        //TO DO
        if (Input.GetKeyUp(KeyCode.Tab))
        {

        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && amountOfCoins > 0)
        {
            GameObject newcoin = Instantiate(coin, transform.position - (transform.forward.normalized * 2.0f), Quaternion.identity);
            amountOfCoins--;
            newcoin.GetComponent<Rigidbody>().useGravity = true;
            newcoin.GetComponent<CoinAnimation>().shouldAnimate = false;
            Vector3 velocity = -transform.forward.normalized * 2.0f;
            newcoin.GetComponent<Rigidbody>().velocity = velocity;
            newcoin.GetComponent<Rigidbody>().angularVelocity = transform.right * -4;
        }
        scoreText.GetComponent<TextMeshProUGUI>().SetText(amountOfCoins.ToString());
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Contains("Coin"))
        {
            c.gameObject.SetActive(false);
            Destroy(c.gameObject);
            amountOfCoins++;
        }
    }
}
