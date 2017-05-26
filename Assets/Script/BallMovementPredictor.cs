using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AAX;


public class BallMovementPredictor : MonoBehaviour {
	private LineRenderer lr;
	private Rigidbody rb;

	public float timeBetweenStep = 1;
	public int stepCount = 50;
	public Vector3 addedSpeed;
	public Vector3 addedForce;
	public bool checkForCollision = true;

	public LayerMask raycastMask = -1;

	[HideInInspector]
	public RaycastHit hitInfo;

	[HideInInspector]
	public List<Vector3> predictionPoints = new List<Vector3> ();


	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		DrawMovementLine();
	}
	/*
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
    */
	private void DrawMovementLine()
	{
		PerformPrediction ();


		Vector3[] res = predictionPoints.ToArray ();
		lr.positionCount = res.Length;
		for (int i = 0; i < res.Length; ++i)
		{
			lr.SetPosition(i, res[i]);
		}

	}

	private void PerformPrediction() {
		float dt = Time.fixedDeltaTime;
		Vector3 v = (rb.isKinematic == false ? rb.velocity : Vector3.zero);
		Vector3 acc = (rb.useGravity && rb.isKinematic == false ? Physics.gravity : Vector3.zero);

		Vector3 addedV = (addedForce / rb.mass) * dt;
		v = v + addedSpeed + addedV;


		bool done = false;
		int iter = 0;
		predictionPoints.Clear ();

		Vector3 pos = rb.transform.position;
		Vector3 toPos = pos;
		Vector3 dir = Vector3.zero;

		while (!done && iter < stepCount) {
			var aDt = acc * dt;
			var dragDt = 1 - rb.drag * dt;
			dragDt = dragDt < 0 ? 0 : dragDt;

			for (int i = 0; i < timeBetweenStep; ++i) {
				v = (v + aDt) * dragDt;
				toPos = pos + v * dt + .5f * acc * dt * dt;
			}

			dir = toPos - pos;
			predictionPoints.Add (pos);

			float dist = Vector3.Distance(pos, toPos);
			if (checkForCollision) {
				Ray ray = new Ray(pos, dir);
				if (Physics.Raycast (ray, out hitInfo, dist, raycastMask)) {
					predictionPoints.Add (hitInfo.point);
					done = true;
				}			
			}

			pos = toPos;
			++iter;

		}

	}



}
