using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour {

    public Transform weaponSlot;
    GameObject gunSpawned;


    float spawnDelay = 1f;
    float timePassed = 0f;

    public bool isShotgun = false;
    public bool isRocket = false;

	// Use this for initialization
	void Start () {
        if(PhotonNetwork.isMasterClient)
            SpawnWeapon();
	}
	
	// Update is called once per frame
	void Update () {
	    if(PhotonNetwork.isMasterClient && weaponSlot.childCount == 0 )
        {
            timePassed += Time.deltaTime;
            if(timePassed >= spawnDelay)
            {
                SpawnWeapon();
                timePassed = 0f;
                spawnDelay = 10f;
            }
        }
	}

    void SpawnWeapon()
    {

        if (isShotgun)
        {
            gunSpawned = PhotonNetwork.Instantiate("ShotgunPickup", weaponSlot.position, weaponSlot.rotation, 0);
        }
        else if (isRocket)
        {
            gunSpawned = PhotonNetwork.Instantiate("RocketPickup", weaponSlot.position, weaponSlot.rotation, 0);
        }

        gunSpawned.transform.parent = weaponSlot;
    }
}
