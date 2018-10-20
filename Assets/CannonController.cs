using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

	public Transform[] cannons;
	public GameObject smokePrefab;

	// Use this for initialization
	void Start () {
		Fire();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Fire(){
		Transform parent = cannons[Random.Range(0,cannons.Length)];
		GameObject clone = Instantiate(smokePrefab);
		clone.transform.position = parent.position;
		clone.transform.parent = parent;
		Debug.Log("ew");
	}
}
