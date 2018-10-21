using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	int totalScore = 0;
	public GameObject[] ScoreTexts;
	public int[] ScoreThresh;

	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void calculateScore(float time){
		int score =(int)(100-Mathf.Abs(time-.15f)*100f/.15f);
		totalScore+=score*10;

		GameObject clone = null;
		for(int i = 0; i<3 && clone == null; i++){
			if(score>=ScoreThresh[i]){
				clone = Instantiate(ScoreTexts[i]);
				clone.transform.position = transform.position;
			}
		}

		text.text = totalScore.ToString();
		//Debug.Log(totalScore);
	}

	public void miss(int pointDeduction = 500){
		totalScore-=pointDeduction;

		GameObject clone = Instantiate(ScoreTexts[3]);
		clone.transform.position = transform.position;
		text.text = totalScore.ToString();
	}
}
