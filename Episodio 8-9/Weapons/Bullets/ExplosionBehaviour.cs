using UnityEngine;
using System.Collections;

public class ExplosionBehaviour : MonoBehaviour {


    float lifeTime = 2f;
    float timePassed = 0f;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if(timePassed >= lifeTime)
        {
            Destroy(gameObject);
        }
	}
}
