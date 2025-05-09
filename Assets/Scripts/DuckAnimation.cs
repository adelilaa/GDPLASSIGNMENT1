using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAnimation : MonoBehaviour
{
    public float bobSpeed = 1f;
    public float bobHeight = 0.25f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

