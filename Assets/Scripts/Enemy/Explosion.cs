using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	[HideInInspector]
	public int dir;
	float directionX;
	float directionY;
	float explosionSpeed = 10;
	float countdown = 0;
	float count = .15f;

	// Use this for initialization
	void Start () {
		countdown = count;

		if (dir == 0) {
			//up
			directionX = 0;
			directionY = 1;
		} else if (dir == 1) {
			//right
			directionX = 1;
			directionY = 0;
		} else if (dir == 2) {
			//down
			directionX = 0;
			directionY = -1;
		} else if (dir == 3) {
			//left
			directionX = -1;
			directionY = 0;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countdown -= Time.fixedDeltaTime;
		if (countdown > 0) {
			transform.Translate (transform.right * Time.fixedDeltaTime * explosionSpeed * directionX + transform.up * Time.fixedDeltaTime * explosionSpeed * directionY);
		} else {
			Destroy (gameObject);
		}
	}
}
