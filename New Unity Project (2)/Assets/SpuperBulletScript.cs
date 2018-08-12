using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpuperBulletScript : MonoBehaviour {

	float speed = 50;

	// Use this for initialization
	void Start () {
		transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 mousePosition;
		mousePosition = Input.mousePosition;          
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
 
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward );
        transform.rotation = rot;  
        transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed);

		if(Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) > 50) {
			Destroy(gameObject);
		}
	}
}

