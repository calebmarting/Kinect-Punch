﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {


	public int missileProbOneIn = 10;
	public GameObject missile;

	public GameObject bomb;
	public GameObject pot;
	public GameObject fish;
	public GameObject cannonTip;

	public Transform[] missileLauncher;

	public Song song;

	private Vector3 spawnPoint;
	public Vector3 shootDirection = new Vector3(0, 0, 1);
	public float shootVelocity = 10f;
	private AudioSource audioData;
	private float offset;
	private bool isGamePlaying;
	private int currentIndex;
	//private object[,] timings;//new object[,] {{7.709289f,'b'},{8.681288f,'b'},{9.645397f,'b'},{10.56776f,'b'},{11.50429f,'b'},{11.93742f,'b'},{12.03765f,'b'},{12.15429f,'b'},{12.27157f,'b'},{12.38761f,'b'}};

	// Use this for initialization
	void Start () {
		//timings = song.timings;

		isGamePlaying = false;
		currentIndex = 0;
		offset = Time.timeSinceLevelLoad;
		spawnPoint = cannonTip.GetComponent<Transform>().position;
		//StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGamePlaying) {
			if (currentIndex < song.times.Length) {
				if (song.times[currentIndex] <= Time.timeSinceLevelLoad - offset-song.offset) {
					if(Random.Range(0,missileProbOneIn)==0){
						GameObject proj = Instantiate(missile);
						proj.transform.position = missileLauncher[Random.Range(0,missileLauncher.Length)].position;
						proj.transform.LookAt(Camera.main.transform.position);
					}
					Spawn(song.types[currentIndex]);
					currentIndex++;
				}
			} else {
				isGamePlaying = false;
			}
		}
	}

	private void Spawn (char type) {
		GameObject projectile;
		if (type == 'b') {
			projectile = Instantiate(bomb, spawnPoint, Random.rotation);
		} else if (type == 'f') {
			projectile = Instantiate(fish, spawnPoint, Random.rotation);
		} else {
			projectile = Instantiate(pot, spawnPoint, Random.rotation);
		}
		// projectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(shootDirection * shootVelocity);
	}

	public void StartGame () {
		audioData = GetComponent<AudioSource>();
		audioData.clip = song.song;
        audioData.Play(0);
		currentIndex = 0;
		offset = Time.timeSinceLevelLoad;
		isGamePlaying = true;
	}

	public void StopGame () {
		audioData = GetComponent<AudioSource>();
        audioData.Stop();
		isGamePlaying = false;
	}

	private void EndGame () {
		isGamePlaying = false;
	}
}