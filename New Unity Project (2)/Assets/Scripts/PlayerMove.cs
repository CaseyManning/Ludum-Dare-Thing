using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float accelerationSpeed = 100;
	float maxSpeed = 3;

	float shotCooldown = 0.2f;

	float shotCooldownTimer = 0.2f;

	public GameObject bullet;
	public GameObject superBullet;

	public int bullets = 30000;

	float spuperCooldown = 5;
	float spuperCooldownTimer = 0;

	float spuperShootingTimer = 0.25f;
	float spuperShootingTimerTimer = 0.25f;
	bool isSpupering = false;

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		shotCooldownTimer -= Time.deltaTime;
		spuperCooldownTimer -= Time.deltaTime;

        if (Input.GetAxis("Fire1") > 0 && shotCooldownTimer < 0 && bullets > 0) {
			GameObject bul = Instantiate(bullet);
			// bullets -= 1;
			GetComponent<AudioSource>().Play();
			Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			shotCooldownTimer = shotCooldown;
			bullet.transform.position = transform.position;
			// Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
		if(Input.GetKey("q") && spuperCooldownTimer < 0) {
			spuperCooldownTimer = spuperCooldown;
			isSpupering = true;
			spuperShootingTimerTimer = spuperShootingTimer;
		}

		if(isSpupering) {
			spuperShootingTimerTimer -= Time.deltaTime;
			GameObject spoop = Instantiate(superBullet);
			spoop.transform.position = transform.position;

			GameObject spoop2 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			spoop2.transform.Rotate(0, 0, 90);

			GameObject spoop3 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			spoop3.transform.Rotate(0, 0, -90);

			if(spuperShootingTimerTimer < 0) {
				isSpupering = false;
			}
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

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
			rigid.velocity = new Vector2(0, 0);
		}
	}
}
