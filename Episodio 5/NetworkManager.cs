using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

    public Transform spawnPoint;


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), PhotonNetwork.connectionStateDetailed.ToString());
    }

	// Use this for initialization
	void Start () {

        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("0.0001 version bla bla");

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
        PhotonNetwork.Instantiate("Player", spawnPoint.position, spawnPoint.rotation, 0);
    }


}
