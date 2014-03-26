using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	GameObject player;
	Environment env;
	float moveSpeed = .089f;
	public float countdown;
	int direction;
	int state = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case 0:
			return;
		case 1:
			//Setup of Door Opening
			GetComponent<BoxCollider2D>().enabled = false;
			env.machinePaused = true;
			player.GetComponent<Rigidbody2D>().isKinematic = true;
			countdown = 1;
			state = 2;
			return;
		case 2:
			//Open the door
			if (countdown > 0) {
				countdown -= Time.deltaTime;
			} else {
				state = 3;
				countdown = .75f;
				if (player.transform.position.x > transform.position.x) {
					direction = -1;
				} else {
					direction = 1;
				}
			}
			return;
		case 3:
			//Move the player
			if (countdown > 0) {
				countdown -= Time.deltaTime;
				player.transform.Translate(transform.right * moveSpeed * direction);
			} else {
				state = 4;
			}
			return;
		case 4:
			//Close the door
			GetComponent<BoxCollider2D>().enabled = true;
			state = 5;
			return;
		case 5:
			//Setup of door closing
			env.machinePaused = false;
			player.GetComponent<Rigidbody2D>().isKinematic = false;
			state = 0;
			return;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			state = 1;
		}
	}
}
