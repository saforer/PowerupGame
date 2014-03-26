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

	Animator anim;
	GameObject player;
	BossStates state = BossStates.teleport;
	Transform bossSpawnTransform;
	Environment env;
	float count = 0;
	float teleportSpeed = 20;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator>();
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
			PreBattleTaunt ();
			break;
		case BossStates.idle:
			Idle();
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
		env.bossHealth = 20;
		state = BossStates.taunt;
		count = 5;

	}

	void PreBattleTaunt() {
		if (count > 0) {
			count -= Time.deltaTime;

		} else {

			state = BossStates.idle;
			count = 2;
		}
	}

	void Idle() {
		if (count > 0) {
			count -= Time.deltaTime;
		} else {
			int newstate = Random.Range(0,5);
			switch (newstate) {
			case 0:
				//Move
				MoveSetup ();
				break;
			case 1:
				//MoveJump
				break;
			case 2:
				//Fire
				break;
			case 3:
				//Fire Jump
				break;
			case 4:
				//Fire Move
				break;
			case 5:
				//Taunt
				break;
			}
		}
	}

	void MoveSetup() {
		//Move
		float moveto = player.transform.position.x;
		if (moveto > transform.position.x) {

		} else {

		}

	}

	void MoveJump() {
		//MoveJump
	}

	void Fire() {
		//Fire
	}

	void FireJump() {
		//Fire Jump
	}

	void FireMove() {
		//Fire Move
	}

	void Taunt() {
		//Taunt
	}
}
