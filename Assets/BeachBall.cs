using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

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
        //transform.position = originalPos;
        //GetComponent<ProjectileThrow>().hasLaunched = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the ball hits a blue duck
        if (collision.gameObject.CompareTag("YellowDuck"))
        {
            // Destroy the duck
            Destroy(collision.gameObject);

            GameManager.instance.DuckHit();

            // Respawn the ball
            Respawn();
        }

        // Check if the ball hits a green duck (bonus)
        else if (collision.gameObject.CompareTag("PinkDuck"))
        {
            // Destroy the duck
            Destroy(collision.gameObject);

            // Add bonus shots via the launcher
            //FindObjectOfType<ProjectileThrow>().AddShot(1);

            GameManager.instance.AddShot(1);

            // Respawn the ball
            Respawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
}