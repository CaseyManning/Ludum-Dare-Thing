using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	float speed = 2;

	bool alive = true;

	int health = 3;

	public Material deathMaterial;

	int score = 0;

	// Use this for initialization
	void Start () {
        
		//GetComponent<Collider2D>()
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 100);
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Wall")) {
			Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
			// float z = transform.rotation.eulerAngles.z;
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

			float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
			float playerY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerY - transform.position.y, playerX - transform.position.x));

            transform.Translate(Vector3.Normalize(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) * Time.deltaTime * speed);
			if(health <= 0) {
				alive = false;
				GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextScript>().score += 5;
				GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextScript>().scoreTimer = 0;
				GetComponent<Renderer>().material = deathMaterial;
				// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().bullets += 5;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.mass = 10;
            }
		}
		if(transform.position.x > -8 && transform.position.x < 8 && transform.position.y < 4 && transform.position.y > -4) {
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Wall")) {
				Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
			}
		}
		
	}
	private void FixedUpdate() {
		// if(transform.localScale.x < 0.05) {
		// 	GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawning>().enemies.Remove(gameObject);
		// 	GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().bullets += 5;
		// 	Destroy(gameObject);
		// }
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet") {
			if(alive) {
				health -= 1;
				Destroy(other.gameObject);
			} else {
                Destroy(other.gameObject);
			}
		} else if(other.gameObject.tag == "SpuperBullet") {
			health -= 1;
		}
		if(other.gameObject.tag == "SpuperBullet" && alive == false) {
			Destroy(gameObject);
		}
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player" && alive) {
			SceneManager.LoadScene("End");
		}
	}
}
