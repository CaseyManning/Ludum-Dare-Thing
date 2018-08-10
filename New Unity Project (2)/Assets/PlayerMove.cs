using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float ySpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		transform.Translate(xSpeed, ySpeed, 0);
	}
}
