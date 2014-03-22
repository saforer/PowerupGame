using UnityEngine;
using System.Collections;

public class SuperChickenAI : MonoBehaviour {
	public float count = 2;
	float countdown = 0;
	public GameObject eggBullet;
	public Transform gunpos;

	// Use this for initialization
	void Start () {
		countdown = count;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countdown -= Time.fixedDeltaTime;
		if (countdown <= 0) {
			Instantiate(eggBullet,gunpos.position,Quaternion.identity);
			countdown = count;
		}
	}

	void DieScript() {

	}
}
