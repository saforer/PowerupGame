﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int playerHealthVal = 2;
	public GameObject explosionSprite;
	Environment env;
	bool died = false;
	// Use this for initialization
	void Start () {
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hurt (int damage) {
		if (playerHealthVal > 1) {
			playerHealthVal -= damage;
		} else {
			if (!died) {
				Die();
			}
		}
	}

	void Die(){
		died = true;
		for (int i=0; i<4; i++) {
			GameObject explosion = Instantiate (explosionSprite, transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponent<Explosion>().dir = i;
		}
		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		env.machinePaused = true;
		Destroy (GetComponent<SpriteRenderer>());
	}
}
