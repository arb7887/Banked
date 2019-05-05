using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float maxJumpHeight;
    public GameObject camera;
    private CharacterController controller;
    private float yVelocity;
    private float gravity = -20f;
    public int coinRequirement;
    [Range(0, 1)]
    public float airMovementPercent; //slider in Unity so we can adjust if we want
    private void Awake(){
        controller = GetComponent<CharacterController>();
    }
    void Update () {
        int coins = GetComponent<CoinManagement>().coinMultiplier;

        Vector2 axisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        axisMovement = axisMovement.normalized;
        Vector3 cameraDirection = camera.transform.position - transform.position;
        cameraDirection = cameraDirection.normalized;
        //Jump if Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
            Jump(coins);

        //Calculate gravity
        if(!controller.isGrounded)
            yVelocity += Time.deltaTime * gravity;

        //Take in the amount of coins for speed
        float newSpeed = Mathf.Clamp(speed - (coins / 2), 0, float.MaxValue);

        Vector3 targetDirection = new Vector3(axisMovement.x, 0f, axisMovement.y);

        //Rotations
        targetDirection = camera.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            transform.rotation = newRotation;
        }

        Vector3 velocity = targetDirection * GetModifiedSpeed(newSpeed);
        //do y velocity seperate to avoid having to multiply by speed
        velocity += Vector3.up * yVelocity;

        controller.Move(velocity * Time.deltaTime);
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

    public void MidAirJump(int coins)
    {
        yVelocity = Mathf.Sqrt(-2 * gravity * Mathf.Clamp(2.0f - (coins / 5.0f), 0, float.MaxValue));
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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.name == "Finish" && GetComponent<CoinManagement>().coinMultiplier >= coinRequirement)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "gate" && GetComponent<CoinManagement>().coinMultiplier >= 3)
        {
            Destroy(collision.gameObject);
            for(int i = 0; i < 3; i++)
            {
                GetComponent<CoinManagement>().PopCoin();
            }
        }
    }

    //Pushable objects
    // https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html
    float pushPower = 4.0f;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
