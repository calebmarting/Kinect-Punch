using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PunchScript : MonoBehaviour {

	public int waitArrSize;
	public float threshold;
	private Vector3[] waitArr;

	private Material mat;

	// Use this for initialization
	void Start () {
		waitArr = new Vector3[waitArrSize];
		mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = waitArr.Length-1; i>0;i--){
			waitArr[i] = waitArr[i-1];
		}
		waitArr[0] = transform.position;
				if(waitArr[0].y-waitArr[waitArr.Length-1].y>threshold){
			//Debug.Log("Swipe " + Mathf.Abs( waitArr[0].z-waitArr[waitArr.Length-1].z));
			mat.color = Color.blue;
		}else if(waitArr[0].z-waitArr[waitArr.Length-1].z>threshold){
			//Debug.Log("Swipe " + Mathf.Abs( waitArr[0].z-waitArr[waitArr.Length-1].z));
			mat.color = Color.red;
		}else
			mat.color = Color.white;


	}

    // void OnTriggerEnter(Collider other)
    // {
	// 	if(other.GetComponent<Rigidbody>()!=null){
    //     	Rigidbody rb = other.GetComponent<Rigidbody>();
	// 		Vector3 dir = Vector3.Normalize(other.transform.position-transform.position);
	// 		var force = 10f;
	// 		rb.AddForce(dir.x*force,dir.y*force,dir.z*force,ForceMode.Impulse);


	// 	}
    // }
}
