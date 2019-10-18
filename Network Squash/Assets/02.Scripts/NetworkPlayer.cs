using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class NetworkPlayer : Photon.MonoBehaviour
{
	public GameObject playerHead;
	Camera cam;
	AudioSource sound;
	
	public GameObject rightHandController;
	public GameObject leftHandController;



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

		if(!photonView.isMine)
		{
			SteamVR_Behaviour_Pose r_Controller = rightHandController.GetComponent<SteamVR_Behaviour_Pose>();
			r_Controller.enabled = false;

			SteamVR_Behaviour_Pose l_Controller = leftHandController.GetComponent<SteamVR_Behaviour_Pose>();
			l_Controller.enabled = false;

		}
    }

    // Update is called once per frame

}
