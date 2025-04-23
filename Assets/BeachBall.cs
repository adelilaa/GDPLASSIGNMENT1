using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : MonoBehaviour
{
    private Rigidbody rb;
    private ThrowingTutorial launcher;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        launcher = FindObjectOfType<ThrowingTutorial>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BlueDuck"))
        {
            Destroy(collision.gameObject);
            RespawnBall();
        }
        else if (collision.gameObject.CompareTag("GreenDuck"))
        {
            Destroy(collision.gameObject);
            if (launcher != null)
            {
                launcher.totalThrows += 3;
                launcher.UpdateShotsUI();
            }
            RespawnBall() ;
        }
    }
    public void RespawnBall()
    {
        if (launcher != null)
        {
            transform.position = launcher.attackPoint.position;
            transform.rotation = launcher.attackPoint.rotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            launcher.ResetThrowExternally();
        }

    }
}
