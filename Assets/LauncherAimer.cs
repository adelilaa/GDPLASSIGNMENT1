//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LauncherAimer : MonoBehaviour
//{
//    public float rotationSpeed = 60f;
//    //public InputActionReference aimAction;

//    private void Update()
//    {
//        Vector2 input = aimAction.action.ReadValue<Vector2>();

//        // Rotate horizontally (Y-axis)
//        transform.Rotate(Vector3.up * input.x * rotationSpeed * Time.deltaTime);

//        // Optional: Add vertical aim if desired
//    }
//}