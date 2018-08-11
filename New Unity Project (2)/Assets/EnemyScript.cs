using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	float speed = 2;

	bool alive = true;

	int health = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
			transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
			if(health <= 0) {
				alive = false;
				 GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("collision");
		if(other.tag == "Bullet") {
			health -= 1;
			Destroy(other.gameObject);
		}
	}
}
