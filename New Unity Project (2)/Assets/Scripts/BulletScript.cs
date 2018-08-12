using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	float speed = 42;

	// Use this for initialization
	void Start () {
		transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 mousePosition;
		mousePosition = Input.mousePosition;          
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //print(mousePosition);

        /*Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward );
        transform.rotation = rot;  
        transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z); benlee was here*/
        transform.LookAt(mousePosition, new Vector3(0, 0, 1));

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed);

		if(Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) > 50) {
			Destroy(gameObject);
		}
	}
}
