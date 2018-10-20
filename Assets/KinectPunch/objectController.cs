using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectController : MonoBehaviour {

	public GameObject deleteOnHit;
	public ParticleSystem playOnHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!playOnHit.isPlaying && deleteOnHit == null)
		{
			Destroy(gameObject);
		}
	}
	
	void onHit (){
		
		Destroy(deleteOnHit);
		playOnHit.Play();
		
	}
	
}
