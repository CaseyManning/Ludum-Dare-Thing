using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	int score;

	float scoreTimer = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreTimer -= Time.deltaTime;
		if(scoreTimer < 0) {
			score += 1;
			GetComponent<Text>().text = "Score: " + score;
			scoreTimer = 1;
		}
	}
}
