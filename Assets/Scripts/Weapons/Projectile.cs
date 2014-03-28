using UnityEngine;
using System.Collections;

public enum Weapon {
	normal
}

public class Projectile : MonoBehaviour {
	public bool playerBullet = true;
	public bool goRight = true;
	public float bulletSpeed = 30;
	float direction = 0;
	int damageAmount = 1;
	public Weapon thisWeapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (goRight) {
			direction = 1;
		} else {
			direction = -1;
		}

		transform.Translate (transform.right * bulletSpeed * Time.deltaTime * direction);
	}

	void OnTriggerEnter2D (Collider2D col) {
		//IF collide with ground, be destroyed
		if (col.CompareTag("Terrain")) {
			Destroy (gameObject);
		}
		if (playerBullet) {
			if (col.CompareTag("Enemy")) {
				col.gameObject.GetComponent<EnemyHealth>().Damage(damageAmount);
				Destroy(gameObject);
			} else 
			if (col.CompareTag("Boss")) {
				col.gameObject.GetComponent<BossHealth>().Damage(thisWeapon);
				Destroy(gameObject);
			}
		} else {
			if (col.CompareTag("Player")) {
				//Hit Player
				col.gameObject.GetComponent<PlayerHealth>().Hurt(damageAmount);
				Destroy(gameObject);
			}
		}
	}
}
