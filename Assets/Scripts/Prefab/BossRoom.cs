using UnityEngine;
using System.Collections;

public class BossRoom : MonoBehaviour {
	GameObject player;
	public GameObject boss;

	public Transform bossRing1;
	public Transform bossRing2;
	public Transform bossSpawn;

	bool waiting = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (waiting) {
			WaitForPlayer();
		}
	}

	void WaitForPlayer () {
		bool A = false;
		bool B = false;
		bool C = false;
		bool D = false;
		
		if (player.transform.position.x > bossRing1.position.x) {A = true;};
		if (player.transform.position.y > bossRing1.position.y) {B = true;};
		if (player.transform.position.x < bossRing2.position.x) {C = true;};
		if (player.transform.position.y < bossRing2.position.y) {D = true;};

		if (A&&B&&C&&D) {
			//HOLY SHIT PLAYER'S HERE TIME TO WAKE UP!
			waiting = false;
			GameObject bossDude = Instantiate(boss,new Vector3(bossSpawn.position.x,bossRing2.position.y,0),Quaternion.identity) as GameObject;

		}
	}
}
