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

    [Header("Audio")]
    public AudioClip splashSound;
    public float splashVolume = 7.0f;

    // Respawn method
    public void Respawn()
    {
        
        //transform.position = originalPos;
        //GetComponent<ProjectileThrow>().hasLaunched = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //check if the ball hits a yellow duck
        if (collision.gameObject.CompareTag("YellowDuck"))
        {
            //play the sound effect  when the ball hits the yellow duck
            PlaySplashSound(collision.transform.position);

            //destroy the duck
            Destroy(collision.gameObject);

            GameManager.instance.DuckHit();

            //respawn the ball
            Respawn();
        }

        //check if the ball hits a pink duck (bonus)
        else if (collision.gameObject.CompareTag("PinkDuck"))
        {
            
            PlaySplashSound(collision.transform.position);

            //destroy the duck
            Destroy(collision.gameObject);

            //add bonus shots via the launcher
            GameManager.instance.AddShot(2);

            //respawn the ball
            Respawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            //turn off objects kinematic upon collision
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void PlaySplashSound(Vector3 position)
    {
        if (splashSound != null)
        {
            //play duck sound at collision
            AudioSource.PlayClipAtPoint(splashSound, position, splashVolume);
        }
    }
}