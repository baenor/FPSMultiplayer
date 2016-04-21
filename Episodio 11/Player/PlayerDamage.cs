using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDamage : Photon.MonoBehaviour {

    int health;
    Text healthText;

	// Use this for initialization
	void Start () {
        if (photonView.isMine)
        {
            health = 100;
            healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
            healthText.text = health.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(photonView.isMine && health <= 0) //Sono morto
        {
            healthText.text = "0".ToString();
            NetworkManager.netManager.PlayerIsDead(); //comunica che sono morto al mio netManager per lo spawn
            PhotonNetwork.Destroy(gameObject);
        }
	}

    //esclusivamente dal master Client
    public void GetDamage(int damage)
    {
        photonView.RPC("ApplyDamage", PhotonTargets.AllViaServer, damage);
    }


    [PunRPC]
    void ApplyDamage(int dmg)
    {
        health -= dmg;
        if (photonView.isMine)
        {
            healthText.text = health.ToString();
        }
    }
}
