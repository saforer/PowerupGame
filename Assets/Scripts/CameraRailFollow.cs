using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRailFollow : MonoBehaviour {
	
	Vector2[] rail;

	List<Vector2[]> railList = new List<Vector2[]>();

	int currentRailIdent = 0;

	Transform player;

	public float xMargin = 1f;
	public float yMargin = 1f;

	public float xSmooth = 8f;
	public float ySmooth = 8f;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").transform;

		rail = new Vector2[4] {
			new Vector2 (6.0f,8.0f), //MinXandY
			new Vector2 (23.8f,8.0f), //MaxXandY
			new Vector2 (28.5f,15.2f), //MinXandY trigger
			new Vector2 (33.5f,15.9f)}; //MaxXandY trigger

		railList.Add(rail);

		rail = new Vector2[4] {
			new Vector2(23.5f,24f),
			new Vector2 (23.5f,24f),
			new Vector2 (13.5f,15.9f),
			new Vector2 (33.5f,31.5f)};

		railList.Add (rail);

		Debug.Log (railList.Count);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TrackPlayer ();

		Debug.Log (currentRailIdent);
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

		if ((player.position.x > currentRail [2].x) && (player.position.y > currentRail [2].y) && !(currentRailIdent > railList.Count)) {
			currentRailIdent++;
		}

		if (player.position.x < currentRail [3].x && player.position.y < currentRail [3].y && !(currentRailIdent < 1)) {
			currentRailIdent--;
		}

		transform.position = new Vector3 (targetX, targetY, -10);
	}
}
