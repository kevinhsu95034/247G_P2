using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehavior : MonoBehaviour
{
    public float speed;
    public float limit;
    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        if(transform.position.x < -limit) {
            transform.position = startingPos;
        }
    }
}
