using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int playerHealthVal = 2;
	public GameObject explosionSprite;
	Environment env;
	// Use this for initialization
	void Start () {
		env = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hurt (int damage) {
		if (playerHealthVal > 1) {
			playerHealthVal -= damage;
		} else {
			Die();
		}
	}

	void Die(){
		Debug.Log ("YOU DIED SON");
		for (int i=0; i<4; i++) {
			GameObject explosion = Instantiate (explosionSprite, transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponent<Explosion>().dir = i;
		}
		env.machinePaused = true;
		Destroy (GetComponent<SpriteRenderer>());
	}
}
