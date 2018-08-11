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
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(50, 100), Random.Range(50, 100)));
		
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
		if(transform.localScale.x < 5) {
			transform.localScale = new Vector3(transform.localScale.x * 1.005f, transform.localScale.y * 1.005f, transform.localScale.z * 1.005f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			SceneManager.LoadScene("End");
		}
	}
}
