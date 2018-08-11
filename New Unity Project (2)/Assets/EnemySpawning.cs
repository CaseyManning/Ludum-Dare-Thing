using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public GameObject basicEnemy;

	float enemyCooldown = 4;
	float enemyCooldownTimer = 4;

	public List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		enemyCooldownTimer -= Time.deltaTime;

		if(enemyCooldownTimer < 0) {
			enemyCooldownTimer = enemyCooldown;

            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
			
		}
	}

    void SpawnEnemy () {

        GameObject enemy = Instantiate(basicEnemy);

        Vector3 spawnPos = new Vector3(Random.Range(-20, 20), Random.Range(-10, 10), 0);
        while (Vector3.Distance(spawnPos, new Vector3(0, 0, 0)) < 12)
        {
            spawnPos = new Vector3(Random.Range(-20, 20), Random.Range(-10, 10), 0);
        }

        enemy.transform.position = spawnPos;
        enemy.transform.LookAt(new Vector3(0, 0, 0));


        for (int i = 0; i < enemies.Count; i++)
        {
            //Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), enemies[i].GetComponent<Collider2D>());
        }
        enemies.Add(enemy);
    }

}

