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

	GameObject spoop2;
	GameObject spoop3;
	GameObject spoop4;
	GameObject spoop5;
	GameObject spoop6;
	GameObject spoop7;

	public int bullets = 30000;

	float spuperCooldown = 5;
	float spuperCooldownTimer = 0;

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
			GameObject spoop = Instantiate(superBullet);
			spoop.transform.position = transform.position;

			spoop2 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop2.transform.Rotate(0, 0, 90);

			spoop3 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop3.transform.Rotate(0, 0, -90);

			spoop4 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop2.transform.Rotate(0, 0, 90);

			spoop5 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop3.transform.Rotate(0, 0, -90);

			spoop6 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop2.transform.Rotate(0, 0, 90);

			spoop7 = Instantiate(superBullet);
			spoop.transform.position = transform.position;
			// spoop3.transform.Rotate(0, 0, -90);

			StartCoroutine(your_timer());
		}
	}

	IEnumerator your_timer() {
		yield return new WaitForSeconds(0.01f);
		spoop2.transform.Rotate(0, 0, 6);
		spoop3.transform.Rotate(0, 0, -6);
		spoop4.transform.Rotate(0, 0, 2);
		spoop5.transform.Rotate(0, 0, -2);
		spoop6.transform.Rotate(0, 0, 4);
		spoop7.transform.Rotate(0, 0, -4);
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
