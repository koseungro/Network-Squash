using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRacket : MonoBehaviour
{
    public float force = 0.2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject target = coll.gameObject;

        Vector3 inNormal = Vector3.Normalize(
            transform.position - target.transform.position);
        Vector3 bounceVector =
            Vector3.Reflect(coll.relativeVelocity, inNormal);
    }
}
