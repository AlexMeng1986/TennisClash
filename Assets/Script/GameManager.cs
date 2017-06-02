using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager = null;


	void Awake() {
		if (null != gameManager) {
			Destroy (gameManager);
		}

		gameManager = this;
		DontDestroyOnLoad (this);
			
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
