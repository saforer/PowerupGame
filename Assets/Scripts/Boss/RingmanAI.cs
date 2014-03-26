using UnityEngine;
using System.Collections;

enum BossStates {
	teleport,
	teleportmove,	
	healthwait,
	taunt,
	idle,
	move,
	jump,
	movejump,
	fire,
	firejump,
	firemove
}

public class RingmanAI : MonoBehaviour {

	GameObject player;
	BossStates state = BossStates.teleport;
	Transform bossSpawnTransform;
	Environment env;

	float teleportSpeed = 5;

	// Use this for initialization
	void Start () {
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
		player = GameObject.FindGameObjectWithTag("Player");
		bossSpawnTransform = GameObject.FindGameObjectWithTag("BossSpawn").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case BossStates.teleport:
			Teleport();
			break;
		case BossStates.teleportmove:
			TeleportMove();
			break;
		case BossStates.healthwait:
			Health ();
			break;
		case BossStates.taunt:
			Taunt ();
			break;
		}
	}




	void Teleport() {
		GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<BoxCollider2D>().enabled = false;
		state = BossStates.teleportmove;
	}

	void TeleportMove() {
		if (transform.position.y > bossSpawnTransform.position.y +1) {
			transform.Translate(transform.up * -1 * teleportSpeed * Time.deltaTime);
		} else {
			state = BossStates.healthwait;
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	void Health() {
		env.bossActive = true;
		if (env.bossHealth <= 21) {
			env.bossHealth++;
		} else {
			state = BossStates.taunt;
		}
	}

	void Taunt() {

	}
}
