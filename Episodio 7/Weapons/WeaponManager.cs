using UnityEngine;
using System.Collections;

public class WeaponManager : Photon.MonoBehaviour {

    enum Weapons
    {
        basicRifle, rocketRifle, shotgun
    };

    enum Ammo
    {
        basicAmmo = 0, rocket = 15, shotgunAmmo = 18
    };

    int weaponEnabled;


    public GameObject basicRifle;
    public GameObject rocketRifle;
    public GameObject shotgun;


	// Use this for initialization
	void Start () {
        weaponEnabled = (int)Weapons.basicRifle;
	}
	
    public int GetWeaponEnabled()
    {
        return weaponEnabled;
    }


    public GameObject GetGOEnabled()
    {
        switch (weaponEnabled)
        {
            case (0):
                return basicRifle;
            case (1):
                return rocketRifle;
            case (2):
                return shotgun;
            default:
                return basicRifle;
        }
    }

    public string GetBulletType(int weaponType)
    {
        switch (weaponType)
        {
            case (0):
                return "BasicBullet";
            case (1):
                return "RocketBullet";
            case (2):
                return "ShotgunBullet";
            default:
                return "BasicBullet";
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (photonView.isMine)
        {
            if(col.tag == "RocketRifle")
            {
                photonView.RPC("DisableWeapon", PhotonTargets.AllBuffered, null);
                photonView.RPC("EnableWeapon", PhotonTargets.AllBuffered, (int)Weapons.rocketRifle);
            }
            if(col.tag == "ShotgunRifle")
            {
                photonView.RPC("DisableWeapon", PhotonTargets.AllBuffered, null);
                photonView.RPC("EnableWeapon", PhotonTargets.AllBuffered, (int)Weapons.shotgun);
            }
        }
    }

    [PunRPC]
    void EnableWeapon(int index)
    {
        weaponEnabled = index;
        GetGOEnabled().SetActive(true);
    }

    [PunRPC]
    void DisableWeapon()
    {
        GetGOEnabled().SetActive(false);
    }

}
