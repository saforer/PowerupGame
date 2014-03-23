using UnityEngine;
using System.Collections;

public class SuperChickenAI : MonoBehaviour {
	public float count = 2;
	float countdown = 0;
	public GameObject eggBullet;
	public Transform gunpos;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		countdown = count;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countdown -= Time.fixedDeltaTime;
		if (countdown <= .5) {
			anim.SetBool("Firing",true);
		}

		if (countdown <= 0) {
			Instantiate(eggBullet,gunpos.position,Quaternion.identity);
			countdown = count;
			anim.SetBool ("Firing",false);
		}
	}

	void DieScript() {

	}
}
