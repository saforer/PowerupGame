using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
	public GameObject bullet;
	public GameObject shootpos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.X)) {
			Shoot();
		}
	}

	void Shoot () {
		GameObject BulletInstance = Instantiate( bullet, shootpos.transform.position, Quaternion.identity) as GameObject;
		BulletInstance.GetComponent<Projectile> ().goRight = GetComponent<PlayerMove> ().facingRight;
	}
}
