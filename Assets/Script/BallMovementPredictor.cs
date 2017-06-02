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

	bool firstTime = true;
	private void DrawMovementLine()
	{
		PerformPrediction ();

		if (firstTime) {
			Vector3 vec = PreformInversePrediction (30, transform.position, Vector3.zero);
			Debug.Log (vec);
		    firstTime = false;
		}


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

		//Debug.Log ("x: " + v.x);
		//Debug.Log ("y: " + v.y);
		//Debug.Log ("z: " + v.z);

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

	Vector3 PreformInversePrediction(int timeStep, Vector3 startPos, Vector3 endPos) {
		Vector3 startVelocity = new Vector3 ();
		startVelocity.x = (endPos.x - startPos.x) / timeStep;
		startVelocity.z = (endPos.z - startPos.z) / timeStep;

		float acc = Mathf.Abs (Physics.gravity.y);
		float dt = Time.fixedDeltaTime;

		if (startPos.y > endPos.y) {
			float accDis = 0.0f;
			int iter = 0;
			while (iter < timeStep) {
				accDis += .5f * acc * dt * dt;
				++iter;
			}
				
			float deltaDis = startPos.y - endPos.y;
			if (isEqual (accDis, deltaDis)) {
				startVelocity.y = 0.0f;
			} else if (accDis > deltaDis) {
				startVelocity.y = (endPos.y - startPos.y) / (timeStep * dt) + .5f * acc * timeStep * dt;
				
			} else {
				float vDis = deltaDis - accDis;
				startVelocity.y = vDis / (timeStep * dt);
			}
				

		} else {
			
		    
		


			
		}




		return startVelocity;
	}

	bool isEqual(float a, float b) {
		if (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon) {
			return true;
		} else {
			return false;
		}
	}

		



}
