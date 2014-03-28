using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BossState {
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

	public GameObject frontBox;

	List<BossState> bossMoves = new List<BossState> () {BossState.movingsetup, BossState.firingsetup, BossState.jumpingsetup};

	Vector2 moveTo;
	Vector2 mySpot;
	public float moveSpeed = 10;

	float count = 0;
	float teleportSpeed = 10;

	bool facingLeft = true;

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
		case BossState.idleloop:
			IdleLoop();
			break;
		case BossState.movingloop:
			MoveLoop();
			break;
		case BossState.jumpingloop:
			JumpingLoop();
			break;
		case BossState.firingloop:
			FiringLoop();
			break;
		default:
			Setup(state);
			break;
		}

		env.bossState = state;

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
		case BossState.idlesetup:
			count = 2;
			state = BossState.idleloop;
			break;
		case BossState.movingsetup:
			count = 1.5f;
			state = BossState.movingloop;
			moveTo = player.transform.position;
			mySpot = transform.position;

			if ((facingLeft) && player.transform.position.x > transform.position.x) {
				Flip();
			}

			if ((!facingLeft) && player.transform.position.x < transform.position.x) {
				Flip();
			}

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
			Debug.LogError("Holy shit my setup switch broke because it got fed " + statein);
			break;
		}
	}

	void MoveLoop() {
		count -= Time.deltaTime;
		int direction = 0;
		if (moveTo.x < mySpot.x) {
			direction = -1;

		}

		if (moveTo.x > mySpot.x) {
			direction = 1;
		}

		if (!Check (frontBox)) {
			transform.Translate (transform.right * direction * moveSpeed * Time.deltaTime);
		}

		if (count <= 0) {
			state = BossState.idlesetup;
		}
	}


	void FiringLoop() {
		state = BossState.idlesetup;
	}


	void JumpingLoop() {
		state = BossState.idlesetup;
	}

	void Flip() {
		//The boss is facing the opposite direction
		facingLeft = !facingLeft;
		
		//The player's sprite is facing the opposite direction
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	bool Check (GameObject obj) {
		bool groundIn = obj.GetComponent<GroundTriggerCheck>().groundInBox;
		return groundIn;
	}
}
