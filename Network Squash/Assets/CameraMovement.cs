using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform cubeTr;
	private Vector3 offset;
	public float delay = 0.2f;
	float myTimer;
	public float smooth = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
		offset =  cubeTr.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		myTimer += Time.deltaTime;
		if (myTimer >= delay)
		{

			transform.position = Vector3.Lerp(transform.position, cubeTr.position - offset, smooth*Time.deltaTime);
			myTimer = 0;
		}

    }
}
