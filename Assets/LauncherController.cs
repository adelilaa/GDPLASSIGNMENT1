using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [Header("References")]
    public Transform launchPoint;
    public GameObject beachBallPrefab;

    [Header("Settings")]
    public float chargeSpeed = 10f;
    public float maxForce = 30f;
    public float upwardForce = 5f;
    public int shotsLeft = 10;
    public float throwCooldown = 1f;

    private float currentForce;
    private bool isCharging = false;
    private bool readyToThrow = true;

    private void Update()
    {
        //stop when ran out of shota
        if (shotsLeft <= 0) return;

        //start charging when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && readyToThrow)
        {
            isCharging = true;
            currentForce = 0f;

        }

        //increase force while holding space
        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0f, maxForce); //Limit force to maxForce
        }

        //launch when space is released
        if (Input.GetKeyUp(KeyCode.Space) && isCharging && readyToThrow)
        {
            Launch();
        }

        // Quit game if Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Launch()
    {
        isCharging = false; // Stop Charging
        readyToThrow = false; // Disable launching until cooldown is over

        // Create the beach ball at the launch point
        GameObject ball = Instantiate(beachBallPrefab, launchPoint.position, launchPoint.rotation);

        // Get the ball's Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Calculate the launch direction and apply upward force
        Vector3 launchDir = launchPoint.forward * currentForce + Vector3.up * upwardForce;
        rb.AddForce(launchDir, ForceMode.Impulse); // Apply force instantly

        shotsLeft--; // Reduce the number of shots
        FindObjectOfType<GameManager>().UpdateShotsUI(shotsLeft); // Update UI

        // Reactivate launching after cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    // Called when hitting a green duck to give bonus shots
    public void AddShots(int amount)
    {
        shotsLeft += amount;
        FindObjectOfType<GameManager>().UpdateShotsUI(shotsLeft);

    }


}