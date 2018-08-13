using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QCooldownText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int cooldown = (int) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().spuperCooldownTimer;
		if(cooldown > 0) {
			GetComponent<Text>().text = "Q Cooldown: " + cooldown;
		} else {
			GetComponent<Text>().text = "Q Cooldown: Ready";
		}
	}
}
