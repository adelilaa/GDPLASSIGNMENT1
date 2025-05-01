using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the beach ball
        if (other.CompareTag("Ball"))
        {
            // Get the BeachBall component attached to the ball
            BeachBall beachBall = other.GetComponent<BeachBall>();

            // If the BeachBall component exists, call the Respawn method
            if (beachBall != null)
            {
                beachBall.Respawn();
            }
        }
    }
}