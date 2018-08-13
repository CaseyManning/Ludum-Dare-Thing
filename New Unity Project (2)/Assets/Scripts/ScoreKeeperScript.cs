using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeperScript : MonoBehaviour {

    public int scoreKept;
    GameObject scoreText;

    // Use this for initialization
    void Start(){
        scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        scoreKept = scoreText.GetComponent<TextScript>().score;
	}
}
