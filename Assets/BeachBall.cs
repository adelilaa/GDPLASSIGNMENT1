using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class BeachBall : MonoBehaviour
{
    // Respawn method
    public void Respawn()
    {
        // Destroy the current ball (it will be respawned by the launcher)
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball hits a blue duck
        if (collision.gameObject.CompareTag("Duck"))
        {
            // Destroy the duck
            Destroy(collision.gameObject);

            // Notify GameManager that a duck was hit
            GameManager.Instance.TargetHit();

            // Respawn the ball
            Respawn();
        }

        // Check if the ball hits a green duck (bonus)
        else if (collision.gameObject.CompareTag("GreenDuck"))
        {
            // Destroy the duck
            Destroy(collision.gameObject);

            // Add bonus shots via the launcher
            FindObjectOfType<LauncherController>().AddShots(3);

            // Notify GameManager as well
            GameManager.Instance.TargetHit();

            // Respawn the ball
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the ball falls off the platform (use a trigger plane below the platforms)
        if (other.gameObject.CompareTag("RespawnZone"))
        {
            Respawn();
        }
    }
}