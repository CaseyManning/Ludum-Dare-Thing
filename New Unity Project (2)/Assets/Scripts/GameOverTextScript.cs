﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTextScript : MonoBehaviour {

    ScoreKeeperScript scoreKeeper;

	// Use this for initialization
	void Start () {
        try {
            scoreKeeper = FindObjectsOfType<ScoreKeeperScript>()[0];
            print("found first!");
        } catch {
            scoreKeeper = FindObjectOfType<ScoreKeeperScript>();
        }
        GetComponent<UnityEngine.UI.Text>().text = "Score: " + scoreKeeper.GetComponent<ScoreKeeperScript>().scoreKept;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
