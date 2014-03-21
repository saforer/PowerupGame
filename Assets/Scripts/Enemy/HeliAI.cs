using UnityEngine;
using System.Collections;

public class HeliAI : MonoBehaviour {
	GameObject player;
	public GameObject reticule;
	Vector3 beginningPos;
	Vector3 targetPos;
	Vector3 desiredVelocity;
	GameObject reticuleInstance;
	bool targeted;
	
	
	const float count = 4;
	float countdown = 0;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countdown -= Time.fixedDeltaTime;

		if ((countdown <= 4 && countdown > 2) && !targeted) {
			target();
			//Draw a sprite at the location showing where the player was when he got scanned
			reticuleInstance = Instantiate( reticule, targetPos, Quaternion.identity)as GameObject;

		}
		
		if (countdown <= 1 && countdown > 0) {


			transform.position += (desiredVelocity * Time.fixedDeltaTime);
		}
		
		if (countdown <= 0) {
			countdown = count;
			targeted = false;
			Destroy(reticuleInstance);
		}
		
	}
	
	void target () {
		beginningPos = transform.position;
		
		targetPos = player.transform.position;
		targetPos.y += 1;

		desiredVelocity = (targetPos - beginningPos);
		

		
		targeted = true;
	}
}
