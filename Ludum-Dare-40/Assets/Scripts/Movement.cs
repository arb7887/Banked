using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created By Andrew Baron for Ludum Dare 40
/// </summary>
public class Movement : MonoBehaviour {
    public float speed = 15.0f;
    private Rigidbody rb;
    private Vector3 position;
    private Vector3 amountToMove;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update () 
    {
        amountToMove = new Vector3(0f, 0f);
        int coins = GetComponent<CoinManagement>().amountOfCoins;
        speed = 15.0f - coins;
        if (Input.GetKey(KeyCode.W) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Vector3.forward, 1.0f)) amountToMove.z += speed;
        if (Input.GetKey(KeyCode.S) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Vector3.back, 1.0f)) amountToMove.z -= speed;
        if (Input.GetKey(KeyCode.A) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Vector3.left, 0.75f)) amountToMove.x -= speed;
        if (Input.GetKey(KeyCode.D) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Vector3.right, 0.75f)) amountToMove.x += speed;
        Move(amountToMove * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            Vector3 upVelocity = Vector3.up * 10.0f;
            upVelocity.y -= coins;
            rb.velocity = upVelocity;
        }
    }
    /// <summary>
    /// Andrew Baron
    /// Purpose: To move the player
    /// </summary>
    /// <param name="translation">Amount to move</param>
    private void Move(Vector3 translation)
    {
        position = transform.position;
        position.x += translation.x;
        position.z += translation.z;

        transform.position = position;
    }
}
