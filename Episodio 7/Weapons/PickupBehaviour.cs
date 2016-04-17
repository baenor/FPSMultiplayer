using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.up * Time.deltaTime * 20f);
	}


    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
