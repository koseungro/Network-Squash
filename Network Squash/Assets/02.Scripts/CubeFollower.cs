using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFollower : MonoBehaviour
{
    public Cube _cubeFollwer;
    private Rigidbody rb;
    private Vector3 _velocity;

    public float _sensitivity = 100f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector3 destination = _cubeFollwer.transform.position;
        rb.transform.rotation = transform.rotation;

        _velocity = (destination - rb.transform.position) * _sensitivity;

        rb.velocity = _velocity;
        transform.rotation = _cubeFollwer.transform.rotation;
    }
    
}
