using UnityEngine;
using System.Collections;

public class PlayerShooting : Photon.MonoBehaviour {

    public Transform firePoint;
    public Transform shellPoint;
    public ParticleSystem muzzle;

    WeaponManager weapons;


    float fireRate = 0.3f;
    float timePassed = 0f;

	// Use this for initialization
	void Start () {
        weapons = GetComponent<WeaponManager>();
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
        string bulletType = weapons.GetBulletType(weapons.GetWeaponEnabled());
        weapons.Shooted();
        //Istanzia un proiettile 
        photonView.RPC("InstantiateBullet", PhotonTargets.All, bulletType,firePoint.position,firePoint.rotation);
        timePassed = 0f;
    }


    [PunRPC]
    void InstantiateBullet(string bullet, Vector3 pos, Quaternion rot)
    {
        Instantiate(Resources.Load(bullet), pos, rot);
        Instantiate(Resources.Load("Shell"), shellPoint.position, shellPoint.rotation);

        muzzle.Play();
    }

    //WeaponManager
    public void SetFireRate(float newValue)
    {
        fireRate = newValue;
    }
}
