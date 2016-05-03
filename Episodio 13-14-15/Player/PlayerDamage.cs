using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDamage : Photon.MonoBehaviour {

    int health;
    Text healthText;


    GameObject killText;

    string possibleKiller;
    bool suicide = false;

	// Use this for initialization
	void Start () {
        if (photonView.isMine)
        {
            killText = GameObject.FindGameObjectWithTag("KillText");
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
    public void GetDamage(int damage, string name)
    {
        photonView.RPC("ApplyDamage", PhotonTargets.AllViaServer, damage,name);
    }


    [PunRPC]
    void ApplyDamage(int dmg, string name)
    {
        health -= dmg;
        possibleKiller = name;
        if (photonView.isMine)
        {
            healthText.text = health.ToString();
        }
    }


    //void OnControllerColliderHit

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EndMap")
        {
            suicide = true;
            health = 0;
        }
    }


    void OnDestroy()
    {
        //Suicidio
        if (suicide)
        {
            killText.GetComponent<KillTextBehaviour>().SetKillerAndVictim(null, photonView.owner.name);
        }
        else
        {
            killText.GetComponent<KillTextBehaviour>().SetKillerAndVictim(possibleKiller, photonView.owner.name);
        }

    }
}
