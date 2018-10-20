using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PostController : MonoBehaviour {

	public UnityEvent OnClick;
	public UnityEvent OnTurnOn;
	public UnityEvent OnTurnOff;

	public Transform button;

	public float timerDuration;
	private float timer=0;

	public bool toggle = false;

	private Animator anim;
	private Material mat;

	public void press(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("ButtonReleased")){
				toggle = !toggle;
				if(toggle){
					mat.color = Color.red;
					OnTurnOn.Invoke();
				}else{
					mat.color = Color.green;
					OnTurnOff.Invoke();
				}
				
				anim.Play("ButtonPress");

				OnClick.Invoke();
		}
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		mat = button.GetComponent<Renderer>().material;
		mat.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {



		
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("ButtonPressed")){
			if(timer == 0){
				timer=timerDuration;
			}else{
				timer -= Time.deltaTime;
			}
			if(timer<0){
				timer = 0;
				anim.Play("ButtonRelease");
			}
		}
				//If touched
		// if(anim.GetCurrentAnimatorStateInfo(0).IsName("ButtonReleased")){
		// 	if(mat.color==Color.red)
		// 		mat.color = Color.green;
		// 	// if(buttonIsColliding){
		// 	// 	anim.Play("ButtonPress");
		// 	// 			mat.color = Color.red;
		// 	// 	OnClick.Invoke();
		// }
		
	}
}
