using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : Photon.MonoBehaviour {

    enum Weapons
    {
        basicRifle, rocketRifle, shotgun
    };

    enum Ammo
    {
        basicAmmo = 0, rocket = 15, shotgunAmmo = 18
    };

    PlayerShooting player;


    int weaponEnabled;
    int currentAmmo;


    public GameObject basicRifle;
    public GameObject rocketRifle;
    public GameObject shotgun;

    Text ammoText;


	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerShooting>();
        weaponEnabled = (int)Weapons.basicRifle;
        player.SetFireRate(0.2f);
        currentAmmo = 0;
        if (photonView.isMine)
        {
            ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
            ammoText.text = "oo".ToString();
        }
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
    public int GetAmmoFromIndex(int index)
    {
        switch (index)
        {
            case (0):
                return 0;
            case (1):
                return 8;
            case (2):
                return 15;
            default:
                return 0;
        }
    }


    //Richiamata da PlayerShooting ogni qual volta si spara
    public void Shooted()
    {
        if(weaponEnabled != 0)
        {
            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
            
            if(currentAmmo == 0)
            {
                photonView.RPC("DisableWeapon", PhotonTargets.AllBuffered, null);
                weaponEnabled = (int)Weapons.basicRifle;
                photonView.RPC("EnableWeapon",PhotonTargets.AllBuffered,weaponEnabled);
                player.SetFireRate(0.2f);
                ammoText.text = "oo".ToString();
            }
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
                player.SetFireRate(0.7f);
            }
            if(col.tag == "ShotgunRifle")
            {
                photonView.RPC("DisableWeapon", PhotonTargets.AllBuffered, null);
                photonView.RPC("EnableWeapon", PhotonTargets.AllBuffered, (int)Weapons.shotgun);
                player.SetFireRate(0.4f);
            }
        }
    }

    [PunRPC]
    void EnableWeapon(int index)
    {
        weaponEnabled = index;
        GetGOEnabled().SetActive(true);
        currentAmmo = GetAmmoFromIndex(index);
        if (photonView.isMine)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }

    [PunRPC]
    void DisableWeapon()
    {
        GetGOEnabled().SetActive(false);
    }

}
