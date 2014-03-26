using UnityEngine;
using System.Collections;

public class GroundTriggerCheck : MonoBehaviour {
	[HideInInspector]
	public bool groundInBox;

	// Use this for initialization
	void Start () {
		groundInBox = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (groundInBox);
		groundInBox = false;
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("Terrain")) {
			groundInBox = true;
		}
	}

}
