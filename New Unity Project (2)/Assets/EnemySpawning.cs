using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public GameObject basicEnemy;

	float enemyCooldown = 3;
	float enemyCooldownTimer = 3;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		enemyCooldownTimer -= Time.deltaTime;

		if(enemyCooldownTimer < 0) {
			enemyCooldownTimer = enemyCooldown;
			GameObject enemy = Instantiate(basicEnemy);
			enemy.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);

			
		}
	}
}
