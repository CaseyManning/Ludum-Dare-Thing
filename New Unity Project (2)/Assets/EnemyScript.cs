using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

	float speed = 2;

	bool alive = true;

	int health = 3;

	// Use this for initialization
	void Start () {
        

        GetComponent<Rigidbody2D>().AddForce(transform.forward * 100);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
			//transform.Translate(Vector3.up * Time.deltaTime * speed);
			if(health <= 0) {
				alive = false;
				 GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			}
		}
		
	}
	private void FixedUpdate() {
		if(transform.localScale.x < 5 && alive) {
			transform.localScale = new Vector3(transform.localScale.x * 1.005f, transform.localScale.y * 1.005f, transform.localScale.z * 1.005f);
		} else {
			transform.localScale = new Vector3(transform.localScale.x * 0.995f, transform.localScale.y * 0.995f, transform.localScale.z * 0.995f);
		}
		if(transform.localScale.x < 0.05) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet") {
			if(alive) {
				health -= 1;
				Destroy(other.gameObject);
			} else {
				
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			SceneManager.LoadScene("End");
		}
	}
}
