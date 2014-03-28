using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {
	public int bossHP = 20;

	public GameObject explosionSprite;

	Environment env;


	// Use this for initialization
	void Start () {
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bossHP <= 0) {
			DieScript();
		} else {
			env.bossHealth = bossHP;
		}


	}

	public void Damage (Weapon thisWeapon){
		if (env.bossActive) {
			bossHP--;
		}
	}

	void DieScript() {
		env.bossActive = false;
		for (int i=0; i<4; i++) {
			GameObject explosion = Instantiate (explosionSprite, transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponent<Explosion>().dir = i;
		}
		Destroy (gameObject);
	}
}
