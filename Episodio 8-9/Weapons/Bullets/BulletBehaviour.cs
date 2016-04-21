using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    [SerializeField]
    float bulletSpeed = 5f;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

	// Use this for initialization
	void Update () {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
	}
	
	
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
