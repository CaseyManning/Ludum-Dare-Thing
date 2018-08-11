using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float accelerationSpeed = 100;
	float maxSpeed = 3;

	float shotCooldown = 0.3f;

	float shotCooldownTimer = 0.3f;

	public GameObject bullet;

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		shotCooldownTimer -= Time.deltaTime;

		if(Input.GetKey("space") && shotCooldownTimer < 0) {
			GameObject bul = Instantiate(bullet);
			shotCooldownTimer = shotCooldown;
			bullet.transform.position = transform.position;
		}
	}
	private void FixedUpdate() {
		// float xSpeed = Input.GetAxis("Horizontal") * speed * 30 * Time.deltaTime;
		// float ySpeed = Input.GetAxis("Vertical") * speed * 30 * Time.deltaTime;
		// rigid.AddForce(new Vector2(xSpeed, ySpeed));

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2(moveHorizontal * accelerationSpeed, moveVertical * accelerationSpeed);
		rigid.AddForce(movement);
		rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);

		if(Input.anyKey == false) {
			rigid.velocity = new Vector2(0, 0);
		}
	}
}
