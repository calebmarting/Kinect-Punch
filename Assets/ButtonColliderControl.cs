using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColliderControl : MonoBehaviour {

	private PostController _listener;

	void Start(){
		_listener = transform.parent.GetComponent<PostController>();
	}
     void OnCollisionEnter(Collision collision)
     {

		 if(collision.gameObject.name.Contains("Hand")){

         _listener.press();
		 }
     }
}
