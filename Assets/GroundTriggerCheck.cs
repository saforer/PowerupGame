using UnityEngine;
using System.Collections;

public class GroundTriggerCheck : MonoBehaviour {
	public bool groundInBox;

	// Use this for initialization
	void Start () {
		groundInBox = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (groundInBox);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Terrain")) {
			groundInBox = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.CompareTag ("Terrain")) {
			groundInBox = false;
		}
	}
}
