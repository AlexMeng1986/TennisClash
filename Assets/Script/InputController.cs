using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {


	void OnSwipe(SwipeGesture gesture)
	{
		SceneLogic.sceneLogic.m_SpeedXZ = gesture.Velocity / 100.0f;

		Vector2 dirXZ = gesture.Move;
		SceneLogic.sceneLogic.m_DirXZ.x = gesture.Move.x / gesture.Move.magnitude;
		SceneLogic.sceneLogic.m_DirXZ.y = gesture.Move.y / gesture.Move.magnitude;

		SceneLogic.sceneLogic.ServeBallMyself ();

		//Debug.Log ("SpeedXZ: " + speedXZ + " DirX: " + dirX + " DirZ: " + dirZ); 
	}

	void OnTap(TapGesture gesture)
	{
		Vector3 posPressed = new Vector3(gesture.Position.x, gesture.Position.y);
		Ray ray = Camera.main.ScreenPointToRay (posPressed);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			SceneLogic.sceneLogic.ActiveLandingPoint (hit.point);
			SceneLogic.sceneLogic.ServeBall ();

		}
		
		Debug.Log ("OnTap.");

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
