using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementByScript : MonoBehaviour 
{

	public float m_Gravity = -9.81f;
	public Vector3 m_Velocity = new Vector3 (0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate() 
	{
		float dt            = Time.fixedDeltaTime;
		Vector3 position    = transform.position;
		MoveBallCaculator.UpdateBallMovement(ref position, ref m_Velocity, m_Gravity, dt);
		transform.position  = position;
	}


	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Court") {
			MoveBallCaculator.BounceBall (ref m_Velocity);
		}
			
	}

}
