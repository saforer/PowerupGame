using UnityEngine;
using System.Collections;

public class EggProjectile : MonoBehaviour {
	Animator anim;

	public float bulletSpeed;
	public GameObject explosionSprite;

	int damageAmount = 1;
	int direction = -1;


	float countdown = 0;
	float count = 4;

	// Use this for initialization
	void Start () {
		countdown = count;
		anim = GetComponent<Animator> ();
		anim.SetBool ("Moving",true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (transform.right * bulletSpeed * Time.fixedDeltaTime * direction);

		countdown -= Time.fixedDeltaTime;

		if (countdown <= 1) {
			direction = 0;
			anim.SetBool ("Moving",false);
		}

		if (countdown <= 0) {
			DieScript();
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag("Terrain")) {
			direction *= -1;

			Vector2 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.collider.CompareTag ("Player")) {
			col.gameObject.GetComponent<PlayerHealth>().Hurt(damageAmount);
			DieScript();
		}
	}

	void DieScript() {
		for (int i=0; i<4; i++) {
			GameObject explosion = Instantiate (explosionSprite, transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponent<Explosion>().dir = i;
		}
		Destroy (gameObject);
	}
}
