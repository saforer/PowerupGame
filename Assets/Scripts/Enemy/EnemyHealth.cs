using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (health <= 0) {
			Die();
		}
	}

	public void Damage(int amount) {
		health -= amount;
	}

	void Die() {
		Destroy (gameObject);
		SendMessage ("DieScript");
	}
}
