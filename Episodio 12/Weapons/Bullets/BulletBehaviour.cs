using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {


    [SerializeField]
    float bulletSpeed = 5f;
    int damage;

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            damage = Random.Range(7, 14);
        }

        Destroy(gameObject, 3f);
    }

	// Use this for initialization
	void Update () {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
	}
	
	
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player" && PhotonNetwork.isMasterClient)
        {
            col.collider.GetComponent<PlayerDamage>().GetDamage(damage);
        }

        Destroy(gameObject);
    }
}
