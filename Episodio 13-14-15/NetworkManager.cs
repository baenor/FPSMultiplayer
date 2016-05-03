using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {

    public static NetworkManager netManager;

    public Transform spawnPoint;
    public Text respawnText;

    bool isDead = false;
    float spawnDelay = 3f;
    float timeNeeded;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public InputField nameField;

    GameObject[] weaponsSpawns;


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), PhotonNetwork.connectionStateDetailed.ToString());
    }

	// Use this for initialization
	void Start () {
        netManager = this;
        timeNeeded = spawnDelay;
        PhotonNetwork.autoJoinLobby = true;

        weaponsSpawns = GameObject.FindGameObjectsWithTag("WeaponSpawn");
	}

    void Update()
    {
        if (isDead)
        {
            respawnText.text = "Respawning..." + Mathf.Round(timeNeeded);
            timeNeeded -= Time.deltaTime;

            if (timeNeeded < 0.5f)
            {
                isDead = false;
                timeNeeded = spawnDelay;
                respawnText.gameObject.SetActive(false);
                RespawnPlayer();
            }
        }
    }
	

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //Ho fallito la funzione JoinRandomRoom...ovvero no stanza trovata
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        PlayerIsDead();
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        if (PhotonNetwork.isMasterClient)
        {
            foreach(GameObject obj in weaponsSpawns)
            {
                obj.GetComponent<WeaponSpawner>().enabled = true;
            }
        }
    }

    //Evocata a inizio gioco e dopo il tempo di spawn 
    void RespawnPlayer()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint.position, spawnPoint.rotation, 0);
    }

    //chiamata da playerDamage
    public void PlayerIsDead()
    {
        isDead = true;
        respawnText.gameObject.SetActive(true);
    }

    public void OnConnectBtnPressed()
    {
        if (nameField.text != "")
        {
            PhotonNetwork.player.name = nameField.text;
            if (!PhotonNetwork.connected)
            {
                PhotonNetwork.ConnectUsingSettings("FPSTutorial 0.1blablaversion EU BEST");
            }
            else
            {
                OnJoinedLobby();
            }
        }
    }

}
