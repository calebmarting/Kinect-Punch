using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PunchScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
		if(other.GetComponent<Rigidbody>()!=null){
        	Rigidbody rb = other.GetComponent<Rigidbody>();
			Vector3 dir = Vector3.Normalize(other.transform.position-transform.position);
			var force = 10f;
			rb.AddForce(dir.x*force,dir.y*force,dir.z*force,ForceMode.Impulse);


		}
    }
}
