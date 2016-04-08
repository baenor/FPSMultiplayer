using UnityEngine;
using System.Collections;

public class PlayerShooting : Photon.MonoBehaviour {

    public Transform firePoint;
    public Transform shellPoint;
    public ParticleSystem muzzle;


    float fireRate = 0.3f;
    float timePassed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (photonView.isMine && Input.GetMouseButton(0) && timePassed >= fireRate)
        {
            Shooting();
        }
	}

    void Shooting()
    {
        //Istanzia un proiettile 
        photonView.RPC("InstantiateBullet", PhotonTargets.All, null);
        timePassed = 0f;
    }


    [PunRPC]
    void InstantiateBullet()
    {
        Instantiate(Resources.Load("BasicBullet"), firePoint.position, firePoint.rotation);
        Instantiate(Resources.Load("Shell"), shellPoint.position, shellPoint.rotation);

        muzzle.Play();
    }
}
