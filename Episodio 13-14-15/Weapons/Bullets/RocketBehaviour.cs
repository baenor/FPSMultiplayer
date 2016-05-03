using UnityEngine;
using System.Collections;

public class RocketBehaviour : MonoBehaviour {

    Rigidbody body;
    float maxDamage = 100f;
    float maxDistance = 5f;

    bool collided = false;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * 1000f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (!collided)
        {
            collided = true;
            Instantiate(Resources.Load("Explosion"), col.contacts[0].point, Quaternion.identity);
            if (PhotonNetwork.isMasterClient)
            {
                Collider[] objectsHit = Physics.OverlapSphere(col.contacts[0].point, maxDistance);
                foreach (Collider obj in objectsHit)
                {
                    if (obj.tag == "Player")
                    {
                        var distance = Vector3.Distance(obj.transform.position, col.contacts[0].point);
                        //calcolo un coefficiente basato sulla distanza, tra 1 e 0, che moltiplico per il mio danno massimo
                        float damage = maxDamage * (1 - (distance/maxDistance)); 
                        obj.GetComponent<PlayerDamage>().GetDamage((int)damage,gameObject.name);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
