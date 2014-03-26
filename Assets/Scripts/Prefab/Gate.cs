using UnityEngine;
using System.Collections;

enum States {
	Idle,
	Preopening,
	Opening,
	MovingPlayer,
	Closing,
	PostClosing
}

public class Gate : MonoBehaviour {
	GameObject player;
	Environment env;
	float moveSpeed = .089f;
	public float countdown;
	States state;
	int direction;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case States.Idle:
			break;
		case States.Preopening:
			//Setup of Door Opening
			GetComponent<BoxCollider2D>().enabled = false;
			env.machinePaused = true;
			player.GetComponent<Rigidbody2D>().isKinematic = true;
			countdown = 1;
			state = States.Opening;
			break;
		case States.Opening:
			//Open the door
			if (countdown > 0) {
				countdown -= Time.deltaTime;
			} else {
				state = States.MovingPlayer;
				countdown = .75f;
				if (player.transform.position.x > transform.position.x) {
					direction = -1;
				} else {
					direction = 1;
				}
			}
			break;
		case States.MovingPlayer:
			//Move the player
			if (countdown > 0) {
				countdown -= Time.deltaTime;
				player.transform.Translate(transform.right * moveSpeed * direction);
			} else {
				state = States.Closing;
			}
			break;
		case States.Closing:
			//Close the door
			GetComponent<BoxCollider2D>().enabled = true;
			state = States.PostClosing;
			break;
		case States.PostClosing:
			//Setup of door closing
			env.machinePaused = false;
			player.GetComponent<Rigidbody2D>().isKinematic = false;
			state = 0;
			break;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			state = States.Opening;
		}
	}
}
