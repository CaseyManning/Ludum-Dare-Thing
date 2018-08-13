using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

	float switchCooldown = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switchCooldown -= Time.deltaTime;

        if(Input.anyKey && switchCooldown < 0){
            SceneManager.LoadScene("StartScene");
        }
	}
}
