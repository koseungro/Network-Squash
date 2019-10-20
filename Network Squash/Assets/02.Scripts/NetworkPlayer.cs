using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class NetworkPlayer : Photon.MonoBehaviour
{
	public GameObject playerHead;
	Camera cam;
	AudioListener sound;

	public GameObject lController;
	public GameObject rController;

	private SteamVR_Behaviour_Pose lHandBehavior;
	private SteamVR_Behaviour_Pose rHandBehavior;

	// Start is called before the first frame update
	void Start()
    {
		if (photonView.isMine)
		{
			cam = playerHead.GetComponent<Camera>();
			cam.enabled = true;

			sound = playerHead.GetComponent<AudioListener>();
			sound.enabled = true;

			// myCamera.SetActive(true);
			//내 손 움직임만 허용
			lHandBehavior = lController.GetComponent<SteamVR_Behaviour_Pose>();
			rHandBehavior = rController.GetComponent<SteamVR_Behaviour_Pose>();
			lHandBehavior.enabled = true;
			rHandBehavior.enabled = true;


		}
    }

    

}
