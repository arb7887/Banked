using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float maxJumpHeight;
    CharacterController controller;
    float yVelocity;
    float gravity = -12f;
    [Range(0, 1)]
    public float airMovementPercent; //slider in Unity so we can adjust if we want
    private void Awake(){
        controller = GetComponent<CharacterController>();
    }
    void Update () {
        int coins = GetComponent<CoinManagement>().amountOfCoins;

        Vector2 axisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 direction = axisMovement.normalized;

        //Jump if Space is pressed
        if (Input.GetKeyDown(KeyCode.Space)) Jump(coins);

        //Calculate gravity
        yVelocity += Time.deltaTime * gravity;

        //Take in the amount of coins for speed
        float newSpeed = Mathf.Clamp(speed - (coins / 2), 0, float.MaxValue);

        Vector3 velocity = new Vector3(direction.x, 0, direction.y) * GetModifiedSpeed(newSpeed);

        //do y velocity seperate to avoid having to multiply by speed
        velocity += Vector3.up * yVelocity; 

        controller.Move(velocity * Time.deltaTime);

        //If it is on the ground, stop using gravity
        if(controller.isGrounded)
        {
            yVelocity = 0;
        }
    }

    /// <summary>
    /// Andrew Baron
    /// Purpose: Allow the player to jump by hitting the space key.
    /// </summary>
    void Jump(int coins)
    {
        if (controller.isGrounded)
        {
            yVelocity = Mathf.Sqrt(-2 * gravity * Mathf.Clamp(maxJumpHeight - (coins / 2), 0, float.MaxValue));
        }
    }

    /// <summary>
    /// Andrew Baron
    /// Purpose: to modify the vertical speed if the player is in the air
    /// </summary>
    /// <param name="s">Speed</param>
    /// <returns>Modified air speed value</returns>
    float GetModifiedSpeed(float s)
    {
        if (controller.isGrounded) return s;
        if (airMovementPercent == 0) return float.MaxValue;
        return s * airMovementPercent;
    }
}
