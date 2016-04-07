using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerNetwork : Photon.MonoBehaviour {

    public Camera myCamera;
    public AudioListener listener;

	// Use this for initialization
	void Start () {
        if (photonView.isMine)
        {
            myCamera.enabled = true;
            listener.enabled = true;
            GetComponent<FirstPersonController>().enabled = true;
        }
	}

}
