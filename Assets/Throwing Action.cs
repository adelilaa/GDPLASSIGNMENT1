using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAction : MonoBehaviour
{
    [ Header(''References'') ]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public TextMeshProUGUI shotsLeftText;

    [Header("Settings")]
    public int totalThrows = 10;
    public float throwCooldown = 1f;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Space;
    public float throwForce = 15f;
    public float throwUpwardForce = 5f;

    private bool readyToThrow = true; 

    private void Start()
    {
        UpdateShotsUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown (throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }
    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Insantiate(objectToThrow, attackPoint.position, cam.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.forward;
        RaycastHit hit;

        if (Physics.Raycast (cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
        UpdateShotsUI();

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow
    {
        readyToThrow = true;
    }

    public void ResetThrowExternally()
    {
        readyToThrow = true;
    }

    public void UpdateShotsUI()
    {
        if (shotsLeftText !=null )
        {
            shotsLeftText.text = "Shots Left:" + totalThrows;
        }
    }
}
