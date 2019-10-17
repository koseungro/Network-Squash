using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
	public GameObject myCamera;



	// Start is called before the first frame update
	void Start()
    {
		if (photonView.isMine)
		{
			myCamera.SetActive(true);
		}
    }

    // Update is called once per frame

}
