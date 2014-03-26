using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRailFollow : MonoBehaviour {
	
	public Vector2[] rail;
	public Vector2[] transfer;
	
	public List<Vector2[]> railList = new List<Vector2[]>();
	
	public List<Vector2[]> transferList = new List<Vector2[]>();
	public List<int> transferListIdent = new List<int>();
	
	public int currentRailIdent = 0;
	int transferTo;
	
	Transform player;
	
	public float xMargin = 1f;
	public float yMargin = 1f;
	
	public float xSmooth = 8f;
	public float ySmooth = 8f;
	
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		RailSetup ();
		TransferSetup ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TrackPlayer ();
		
		TransferCheck ();
	}
	
	bool CheckXMargin()	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}
	
	bool CheckYMargin()	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}
	
	void TrackPlayer () {
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		Vector2[] currentRail = railList [currentRailIdent];
		
		if (CheckXMargin()) {
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
		}
		
		if (CheckYMargin()) {
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);
		}
		
		targetX = Mathf.Clamp(targetX, currentRail[0].x, currentRail[1].x);
		targetY = Mathf.Clamp(targetY, currentRail[0].y, currentRail[1].y);
		
		if (Input.GetKeyDown (KeyCode.A) && currentRailIdent < railList.Count-1) {
			currentRailIdent++;
		}
		
		if (Input.GetKeyDown (KeyCode.S) && currentRailIdent > 0) {
			currentRailIdent--;
		}
		
		transform.position = new Vector3 (targetX, targetY, -10);
	}
	
	void RailSetup () {
		rail = new Vector2[2] {
			new Vector2 (6.3f, 8.0f), //MinXandY
			new Vector2 (23.8f, 8.0f)}; //MaxXandY
		//Rail 0 - Start
		railList.Add(rail);
		
		rail = new Vector2[2] {
			new Vector2 (23.5f, 24f),
			new Vector2 (23.5f, 24f)};
		//Rail 1 - S bend
		railList.Add (rail);
		
		rail = new Vector2[2] {
			new Vector2 (45.5f, 24f),
			new Vector2 (45.5f, 24f)};
		//Rail 2 - room before boss corridor
		railList.Add (rail);
		
		rail = new Vector2[2] {
			new Vector2 (67.5f, 24f),
			new Vector2 (67.5f, 24f)};
		//Rail 3 - Boss Corridor
		railList.Add (rail);
		
		rail = new Vector2[2] {
			new Vector2 (89.5f, 24f),
			new Vector2 (89.5f, 24f)};
		//Rail 4 - Boss room
		railList.Add (rail);
	}
	
	void TransferSetup () {
		transfer = new Vector2[2] {
			new Vector2 (28.5f, 15f),
			new Vector2 (33.5f, 16f)};
		transferTo = 0;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);
		
		transfer = new Vector2[2] {
			new Vector2 (28.5f, 16f),
			new Vector2 (33.5f, 17f)};
		transferTo = 1;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (33.5f, 24.9f),
			new Vector2 (34.5f, 29f)};
		transferTo = 1;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (34.5f, 24.9f),
			new Vector2 (35.5f, 29f)};
		transferTo = 2;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (55.5f, 16.9f),
			new Vector2 (56.5f, 21f)};
		transferTo = 2;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (56.5f, 16.9f),
			new Vector2 (57.5f, 21f)};
		transferTo = 3;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (77.5f, 16.9f),
			new Vector2 (78.5f, 21f)};
		transferTo = 3;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

		transfer = new Vector2[2] {
			new Vector2 (78.5f, 16.9f),
			new Vector2 (79.5f, 21f)};
		transferTo = 4;
		
		transferList.Add (transfer);
		transferListIdent.Add (transferTo);

	}
	
	void TransferCheck () {
		for (int i = 0; i<transferList.Count; i++) {
			Vector2[] tranCheck = transferList[i];

			bool A = false;
			bool B = false;
			bool C = false;
			bool D = false;

			if (player.position.x > tranCheck[0].x) {A = true;}; //Are they to the right of the bottom left point
			if (player.position.y > tranCheck[0].y) {B = true;}; //Are they above the bottom left point
			if (player.position.x < tranCheck[1].x) {C = true;}; //Are they to the left of the top right point
			if (player.position.y < tranCheck[1].y) {D = true;}; //Are they below the top right point

			if (A && B && C && D) {
				//They're in the box

				//Set the camera rail to the place the transfer says to go too
				currentRailIdent = transferListIdent[i];
			}

		}
	}

}
