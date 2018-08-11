using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

	float speed = 3;

	bool alive = true;

	int health = 3;

	public Material deathMaterial;

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
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
			if(health <= 0) {
				alive = false;
				GetComponent<Renderer>().material = deathMaterial;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().bullets += 5;
            }
		}
		if(transform.position.x > -8 && transform.position.x < 8 && transform.position.y < 4 && transform.position.y > -4) {
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Wall")) {
				Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
			}
		}
		
	}
	private void FixedUpdate() {
		if(transform.localScale.x < 1 && alive) {
			transform.localScale = new Vector3(transform.localScale.x * 1.005f, transform.localScale.y * 1.005f, transform.localScale.z * 1.005f);
        } else if(transform.localScale.x > 0) {
			transform.localScale = new Vector3(transform.localScale.x * 0.997f, transform.localScale.y * 0.997f, transform.localScale.z * 0.997f);
        } else {
         
        }
		if(transform.localScale.x < 0.05) {
			GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawning>().enemies.Remove(gameObject);
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().bullets += 5;
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet") {
			if(alive) {
				health -= 1;
				Destroy(other.gameObject);
			} else {
                Destroy(other.gameObject);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player" && alive) {
			SceneManager.LoadScene("End");
		}
	}
}
