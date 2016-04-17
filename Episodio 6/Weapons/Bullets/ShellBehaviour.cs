using UnityEngine;
using System.Collections;

public class ShellBehaviour : MonoBehaviour {

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.right *4f, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.localScale == Vector3.zero)
        {
            Destroy(gameObject);
        }
	}
}
