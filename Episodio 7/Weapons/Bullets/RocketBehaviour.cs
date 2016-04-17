using UnityEngine;
using System.Collections;

public class RocketBehaviour : MonoBehaviour {

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * 1000f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        Instantiate(Resources.Load("Explosion"), col.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
    }
}
