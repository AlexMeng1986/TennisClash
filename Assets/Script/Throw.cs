using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 vel = PreformInversePrediction (10, transform.position, Vector3.zero);

		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb) {
			rb.velocity = vel;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 PreformInversePrediction(int timeStep, Vector3 startPos, Vector3 endPos) {
		float dt = Time.fixedDeltaTime;

		Vector3 startVelocity = new Vector3 ();
		startVelocity.x = (endPos.x - startPos.x) / (timeStep * dt);
		startVelocity.z = (endPos.z - startPos.z) / (timeStep * dt);

		float acc = Mathf.Abs (Physics.gravity.y);

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
