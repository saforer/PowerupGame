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
	bool beginMotion = false;
	
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
			DrawReticule();

		}
		
		if (countdown <= 1 && countdown > 0) {
			if (!beginMotion) {
				beginningPos = transform.position;
				beginMotion = true;
			}


			desiredVelocity = (targetPos - beginningPos);

			transform.position += (desiredVelocity * Time.fixedDeltaTime);

		}
		
		if (countdown <= 0) {
			countdown = count;
			targeted = false;
			beginMotion = false;
			Destroy(reticuleInstance);
		}
		
	}

	void DrawReticule() {
		targeted = true;

		targetPos = player.transform.position;
		targetPos.y += 1;

		reticuleInstance = Instantiate( reticule, targetPos, Quaternion.identity)as GameObject;
	}

	void DieScript() {
		Destroy (reticuleInstance);
	}
}