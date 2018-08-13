using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    // Below code destroys scorekeeper after end scene (when StartMenu is loaded)
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            Destroy(this.gameObject);
        }
    }

}
