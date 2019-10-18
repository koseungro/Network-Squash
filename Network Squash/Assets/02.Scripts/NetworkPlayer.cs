using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
	public GameObject playerHead;
	Camera cam;
	AudioSource sound;



	// Start is called before the first frame update
	void Start()
    {
		if (photonView.isMine)
		{
			cam = playerHead.GetComponent<Camera>();
			cam.enabled = true;

			sound = playerHead.GetComponent<AudioSource>();
			sound.enabled = true;
			// myCamera.SetActive(true);
		}
    }

    // Update is called once per frame

}
