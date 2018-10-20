using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroyOnComplete : MonoBehaviour {

	private Animator anim;
	public PunchScript handLeft;
	public PunchScript handRight;
	public float fuse;
	public float fuseGracePeriod;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		handLeft = GameObject.Find("Left Hand").GetComponent<PunchScript>();
				handRight = GameObject.Find("Right Hand").GetComponent<PunchScript>();
	}
	
	// Update is called once per frame
	void Update () {

		fuse = fuse-Time.deltaTime;
		if(fuse<=0){
			if(fuse < 0){
				fuseGracePeriod = fuseGracePeriod+fuse;//if it is less than 0 
				fuse=0;
			}else{
				fuseGracePeriod -=Time.deltaTime;
			}

			if(handLeft.isPunching()||handRight.isPunching()){
				if(fuseGracePeriod>0&&fuse==0){
					Debug.Log("PUNCH KICK");
					Destroy(gameObject);
				}
			}
			
		}



		if(anim!=null){
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Finished")){
				Destroy(gameObject);
			}
		}
	}
}
