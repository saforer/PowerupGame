using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum BossState {
	teleportingIn,
	healthSetup,
	battletaunt,
	idleloop,
	idlesetup,
	movingsetup,
	movingloop,
	firingsetup,
	firingloop,
	jumpingsetup,
	jumpingloop
}



public class RingmanAI : MonoBehaviour {

	Animator anim;
	GameObject player;
	BossState state = BossState.teleportingIn;
	Transform bossSpawnTransform;
	Environment env;

	List<BossState> bossMoves = new List<BossState> () {BossState.movingsetup, BossState.firingsetup, BossState.jumpingsetup};

	float count = 0;
	float teleportSpeed = 10;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator>();
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
		player = GameObject.FindGameObjectWithTag("Player");
		bossSpawnTransform = GameObject.FindGameObjectWithTag("BossSpawn").GetComponent<Transform>();
		GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case BossState.teleportingIn:
			TeleportIn();
			break;
		case BossState.healthSetup:
			healthSetup();
			break;
		case BossState.idlesetup:
			IdleSetup();
			break;
		case BossState.idleloop:
			IdleLoop();
			break;
		case BossState.movingloop:
			MoveLoop();
			break;
		default:
			Setup(state);
			break;
		}


	}

	void TeleportIn() {
		transform.Translate (transform.up * -1 * teleportSpeed * Time.deltaTime);
		if (transform.position.y < bossSpawnTransform.position.y) {
			state = BossState.healthSetup;
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	void healthSetup() {
		env.bossActive = true;
		env.bossHealth = 20;
		state = BossState.idlesetup;
	}

	void IdleSetup() {
		count = 4;
		state = BossState.idleloop;
	}

	void IdleLoop() {
		count -= Time.deltaTime;
		if (count <= 0) {
			state = ChooseAction();
		}
	}

	BossState ChooseAction() {
		return bossMoves[(int) Random.Range(0,bossMoves.Count)];
	}

	void Setup( BossState statein) {
		switch (statein) {
		case BossState.movingsetup:
			count = 2;
			state = BossState.movingloop;
			break;
		case BossState.firingsetup:
			count = 2;
			state = BossState.firingloop;
			break;
		case BossState.jumpingsetup:
			count = 2;
			state = BossState.jumpingloop;
			break;
		default:
			Debug.LogError("Holy shit my setup switch broke because it got fed" + statein);
			break;
		}
	}

	void MoveLoop() {
		count -= Time.deltaTime;
		Debug.Log ("Moving Out");
		if (count <= 0) {
			state = BossState.idlesetup;
		}
	}


	void FiringLoop() {

	}


	void JumpingLoop() {

	}
}
