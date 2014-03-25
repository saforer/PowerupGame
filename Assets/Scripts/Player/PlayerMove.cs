using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;
	float playerSpeed = 15;
	bool jumping = false;
	float jumpPower = 20;

	bool onGround = false;
	public GameObject groundObject;

	bool backOpen = false;
	public GameObject backObject;

	bool frontOpen = false;
	public GameObject frontObject;

	bool leftOk = false;
	bool rightOk = false;

	public GameObject bullet;
	public GameObject shootpos;

	void Start () {
	
	}

	void FixedUpdate () {


	}

	// Update is called once per frame
	void Update () {
		//Left and Right Movement
		if (Input.GetKey(KeyCode.LeftArrow)) {
			backOpen = !Check (backObject);
			frontOpen = !Check (frontObject);
			
			if (facingRight) {
				leftOk = backOpen;
			} else {
				leftOk = frontOpen;
			}

			if (leftOk) {
				transform.Translate (transform.right * playerSpeed * -1 * Time.fixedDeltaTime);
				if (facingRight) {
					Flip();
				}
			}
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			backOpen = !Check (backObject);
			frontOpen = !Check (frontObject);
			
			if (facingRight) {
				rightOk = frontOpen;
			} else {
				rightOk = backOpen;
			}

			if (rightOk) {
				transform.Translate (transform.right * playerSpeed * 1 * Time.fixedDeltaTime);
				if (!facingRight) {
					Flip();
				}
			}
		}

		//Jump
		if (Input.GetKeyDown (KeyCode.Z)) {
			onGround = Check (groundObject);

			//if (onGround) {
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpPower);
				jumping = true;
			//}
		}

		if (Input.GetKeyUp (KeyCode.Z) && jumping && rigidbody2D.velocity.y > 0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0);
			jumping = false;
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			Shoot();
		}

	}

	void Flip() {
		//The player is facing the opposite direction
		facingRight = !facingRight;

		//The player's sprite is facing the opposite direction
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	bool Check (GameObject obj) {
		bool groundIn = obj.GetComponent<GroundTriggerCheck>().groundInBox;
		return groundIn;
	}

	void Shoot () {
		GameObject BulletInstance = Instantiate( bullet, shootpos.transform.position, Quaternion.identity) as GameObject;
		BulletInstance.GetComponent<Projectile> ().goRight = GetComponent<PlayerMove> ().facingRight;
	}
}
