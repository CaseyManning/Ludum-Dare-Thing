using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float speed = 10;

	float shotCooldown = 0.3f;

	float shotCooldownTimer = 0.3f;

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}	
	
	// Update is called once per frame
	void Update () {

		shotCooldownTimer -= Time.deltaTime;

		float xSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float ySpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		transform.Translate(xSpeed, ySpeed, 0);

		if(Input.GetKey("space") && shotCooldownTimer < 0) {
			GameObject bul = Instantiate(bullet);
			shotCooldownTimer = shotCooldown;
			bullet.transform.position = transform.position;
		}
	}
}
