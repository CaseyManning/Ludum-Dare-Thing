using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public GameObject basicEnemy;

	float waveCooldown = 4;
	float waveCooldownTimer = 0;
    float spawnsPerWave = 1.0f;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		waveCooldownTimer -= Time.deltaTime;

		if(waveCooldownTimer < 0) {
			waveCooldownTimer = waveCooldown;

            for (int i = 0; i < spawnsPerWave; i++){
                SpawnEnemy();
            }

            spawnsPerWave += 0.3f;
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
    }

}

