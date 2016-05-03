using UnityEngine;
using System.Collections;

public class PlayerShooting : Photon.MonoBehaviour {

    public Transform firePoint;
    public Transform shellPoint;
    public ParticleSystem muzzle;

    WeaponManager weapons;


    float fireRate = 0.3f;
    float timePassed = 0f;

    public AudioClip normal, shotgun, rocket;
    AudioSource source;

	// Use this for initialization
	void Start () {
        weapons = GetComponent<WeaponManager>();
        source = GetComponent<AudioSource>();
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
        int weaponType = weapons.GetWeaponEnabled();
        string bulletType = weapons.GetBulletType(weaponType);
        weapons.Shooted();
        //Istanzia un proiettile 
        photonView.RPC("InstantiateBullet", PhotonTargets.All, bulletType,firePoint.position,firePoint.rotation,photonView.owner.name,weaponType);
        timePassed = 0f;
    }


    [PunRPC]
    void InstantiateBullet(string bullet, Vector3 pos, Quaternion rot, string playerName,int weaponType)
    {
        GameObject projectile = (GameObject)Instantiate(Resources.Load(bullet), pos, rot);
        projectile.name = playerName;
        Instantiate(Resources.Load("Shell"), shellPoint.position, shellPoint.rotation);

        if (weaponType == 0)
        {
            source.clip = normal;
        }
        else if(weaponType == 1)
        {
            source.clip = rocket;
        }
        else if(weaponType == 2)
        {
            source.clip = shotgun;
        }

        muzzle.Play();
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    //WeaponManager
    public void SetFireRate(float newValue)
    {
        fireRate = newValue;
    }
}
