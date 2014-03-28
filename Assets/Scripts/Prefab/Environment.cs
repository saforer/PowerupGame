using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	GameObject player;
	int pHP;
	public bool playerPaused = false;
	public bool machinePaused = false;
	public bool bossActive = false;
	public int bossHealth = 0;
	public BossState bossState;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		pHP = player.GetComponent<PlayerHealth>().playerHealthVal;
	}

	void OnGUI () {
		if (pHP > 0) {
			GUI.Box (new Rect (0,0,100,30), "Health :" + pHP);
		}
		if (bossActive) {
			GUI.Box (new Rect (Screen.width - 100,0,100,30), "Health :" + bossHealth);
			GUI.Box (new Rect (Screen.width - 200, Screen.height - 30,200,30), "State :" + bossState);
		}

		if (playerPaused) {
			GUI.Box (new Rect (Screen.width/2 - 50,Screen.height/2 - 15,100,30), "Paused");
		}
	}
}
