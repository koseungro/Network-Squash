using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Cube : MonoBehaviour
{
    public CubeFollower _cubeFollower;
    CubeFollower follower;


    void Start()
    {
        MakeCubeFollower();
    }
    
    void Update() {
        follower.transform.position = transform.position;
    }


    void MakeCubeFollower()
    {
        follower = Instantiate(_cubeFollower);
        follower.transform.position = transform.position;
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "BALL") {
            Vector3 vel = follower.GetComponent<Rigidbody>().velocity;
            Vector3 angularVel = follower.GetComponent<Rigidbody>().angularVelocity;
        }
    }
}
