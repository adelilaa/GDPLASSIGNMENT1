using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(ProjectileThrow))]
public class BeachBall : MonoBehaviour
{
    private Vector3 originalPos = Vector3.zero;
    private void Awake()
    {
        originalPos = transform.position;
    }

    // Respawn method
    public void Respawn()
    {
        // Destroy the current ball (it will be respawned by the launcher)
        transform.position = originalPos;
        GetComponent<ProjectileThrow>().hasLaunched = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the ball hits a blue duck
        if (collision.gameObject.CompareTag("YellowDuck"))
        {
            // Destroy the duck
            Destroy(collision.gameObject);

            // Notify GameManager that a duck was hit
            GameManager.Instance.TargetHit();

            // Respawn the ball
            Respawn();
        }

        // Check if the ball hits a green duck (bonus)
        else if (collision.gameObject.CompareTag("PinkDuck"))
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
}