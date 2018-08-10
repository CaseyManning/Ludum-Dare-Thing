using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	float speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
}
