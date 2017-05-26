using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AAX;

public class BallMovementPredictor : MonoBehaviour {
	private LineRenderer lr;
	private Rigidbody rb;

	public float timeBetweenStep = 1;
	public int stepCount = 50;


	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		DrawMovementLine();
	}

	private void DrawMovementLine()
	{
		var res = rb.CalculateMovement(stepCount, timeBetweenStep);

		lr.positionCount = stepCount + 1;
		lr.SetPosition(0, transform.position);
		for (int i = 0; i < res.Length; ++i)
		{
			lr.SetPosition(i+1, res[i]);
		}

	}



}
