using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;
	float playerSpeed = 20;
	bool jumping = false;
	float jumpPower = 20;

	bool onGround = false;
	public GameObject groundObject;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		onGround = false;
	}

	// Update is called once per frame
	void Update () {
		//Left and Right Movement
		if (Input.GetKey(KeyCode.LeftArrow) ) {

			transform.Translate (transform.right * playerSpeed * -1 * Time.deltaTime);
			if (facingRight) {
				Flip();
			}
		}

		if (Input.GetKey(KeyCode.RightArrow) ) {
			transform.Translate (transform.right * playerSpeed * 1 * Time.deltaTime);
			if (!facingRight) {
				Flip();
			}
		}

		//Jump
		if (Input.GetKeyDown (KeyCode.Z) && onGround) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpPower);
			jumping = true;
		}

		if (Input.GetKeyUp (KeyCode.Z) && jumping && rigidbody2D.velocity.y > 0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0);
			jumping = false;
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

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag("Terrain")) {
			onGround = true;
		}
	}

}
