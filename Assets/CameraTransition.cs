using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform menuPosition;
    public Transform gamePosition;
    public float transitionSpeed = 2f;

    private bool moveToGame = false;

    private void Start()
    {
        // Camera set at the starting ( menu ) position
        transform.position = menuPosition.position;
        transform.rotation = menuPosition.rotation;
    }
    private void Update()
    {
        // When any key is pressed, transition to game position camera
        if (!moveToGame && Input.anyKeyDown)
        {

            moveToGame = true;

        }

        // Smooth animation from menu camera to game position camera
        if (moveToGame)
        {
            transform.position = Vector3.Lerp(transform.position, gamePosition.position, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, gamePosition.rotation, Time.deltaTime * transitionSpeed);

        }
    }
}