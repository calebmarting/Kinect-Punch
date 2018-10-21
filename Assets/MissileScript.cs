using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position+=transform.forward*Time.deltaTime*speed;
	}

	void OnCollisionEnter(Collision hit){
		if(hit.gameObject.name == "CameraEmpty"){
			GameObject.Find("ScoreManager").GetComponent<ScoreManager>().miss(5000);		
			Destroy(gameObject);
		}
	}

}
