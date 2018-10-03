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
    private Queue<CoinType> coinQueue;

    public int coinMultiplier;
    public int amountOfNegativeCoins;
    public GameObject coin;
    public GameObject negativeCoin;
    public GameObject scoreText;
    void Awake()
    {
        coinQueue = new Queue<CoinType>();
        amountOfNegativeCoins = 0;
        coinMultiplier = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && coinQueue.Count != 0)
        {
            CoinType poppedCoin = coinQueue.Dequeue();
            GameObject currentCoin = coin;
            if (poppedCoin == CoinType.Negative)
            {
                currentCoin = negativeCoin;
                amountOfNegativeCoins--;
            }
            GameObject newcoin = Instantiate(currentCoin, transform.position - (transform.forward.normalized * 2.0f), Quaternion.identity);
            newcoin.GetComponent<Rigidbody>().useGravity = true;
            newcoin.GetComponent<CoinAnimation>().shouldAnimate = false;
            Vector3 velocity = -transform.forward.normalized * 2.0f;
            newcoin.GetComponent<Rigidbody>().velocity = velocity;
            newcoin.GetComponent<Rigidbody>().angularVelocity = transform.right * -4;
            coinMultiplier = coinQueue.Count - (amountOfNegativeCoins * 2);
        }
        //scoreText.GetComponent<TextMeshProUGUI>().SetText(amountOfCoins.ToString());
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Contains("Coin"))
        {
            switch(c.gameObject.tag)
            {
                case "Coin":
                    coinQueue.Enqueue(CoinType.Normal);
                    break;
                case "NegativeCoin":
                    coinQueue.Enqueue(CoinType.Negative);
                    amountOfNegativeCoins++;
                    break;
            }
            c.gameObject.SetActive(false);
            Destroy(c.gameObject);
        }
        coinMultiplier = coinQueue.Count - (amountOfNegativeCoins * 2);
    }
}
