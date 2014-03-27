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
	bool setupStage = true;

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
		case BossStates.idle:
			Idle();
			break;
		case BossStates.move:
			Move();
			break;
		case BossStates.movejump:
			MoveJump ();
			break;
		case BossStates.fire:
			Fire ();
			break;
		case BossStates.firejump:
			FireJump ();
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
		env.bossHealth = 20;
		state = BossStates.idle;
	}


	void Idle() {
		if (!setupStage) {
				if (count > 0) {
					count -= Time.deltaTime;
				} else {
					setupStage = true;
					int newstate = Random.Range (0, 4);
					switch (newstate) {
						case 0:
							//Move
							state = BossStates.move;
							break;
						case 1:
							//MoveJump
							state = BossStates.movejump;
							break;
						case 2:
							//Fire
							state = BossStates.fire;
							break;
						case 3:
							//Fire Jump
							state = BossStates.firejump;
							break;
						case 4:
							//Fire Move
							state = BossStates.firemove;
							break;
					}
				}
		} else {
			setupStage = false;
			count = 4;
			Debug.Log ("IDLE START");
		}
	}

	void Move() {
		if (!setupStage) { 
			//Move
			Debug.Log ("Moving");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}

	}

	void MoveJump() {
		if (!setupStage) { 
			//Move & Jump
			Debug.Log ("Move & Jump");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}
	}

	void Fire() {
		if (!setupStage) { 
			//Fire
			Debug.Log ("Fire");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}
	}

	void FireJump() {
		if (!setupStage) { 
			//Fire & Jump
			Debug.Log ("Fire & Jump");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}
	}

	void FireMove() {
		if (!setupStage) { 
			//Fire & Move
			Debug.Log ("Fire & Move");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}
	}

	void Taunt() {
		if (!setupStage) { 
			//Taunt
			Debug.Log ("Taunting");
			setupStage = true;
			state = BossStates.idle;
		} else {
			setupStage = false;
			
		}
	}
}
