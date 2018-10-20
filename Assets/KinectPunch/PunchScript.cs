using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PunchScript : MonoBehaviour {

	public int waitArrSize;
	public float threshold;
	private Vector3[] waitArr;

private bool isPunch = false;
	private Material mat;

	// Use this for initialization
	void Start () {
		waitArr = new Vector3[waitArrSize];
		mat = transform.GetComponent<Renderer>().material;
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
			isPunch = true;
		}else if(waitArr[0].z-waitArr[waitArr.Length-1].z>threshold){
			//Debug.Log("Swipe " + Mathf.Abs( waitArr[0].z-waitArr[waitArr.Length-1].z));
			mat.color = Color.red;
			isPunch = true;
		}else{
			mat.color = Color.white;
			isPunch = false;
		}

	}

	public bool isPunching(){
		return isPunch;
	}


}
