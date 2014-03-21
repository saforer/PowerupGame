using UnityEngine;
using System.Collections;

public class GroundTriggerCheck : MonoBehaviour {
	public bool groundInBox = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (groundInBox);
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("Terrain")) {
			groundInBox = true;
		} else {
			groundInBox = false;
		}
	}
}
