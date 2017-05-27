using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    private Rigidbody rb;
    private float height;
    private float latest = 0f;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        height = 5;
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        float now = Time.realtimeSinceStartup;
        if (now - latest < 0.1f)
        {
            return;
        }
        latest = now;

        if (collision.gameObject.tag == "Racket" || collision.gameObject.tag == "Wall")
        {
            int directionMod = collision.gameObject.tag == "Wall" ? 1 : -1;
            float x = rb.transform.position.x > -1 && rb.transform.position.x < 1 ? 0 : rb.transform.position.x;
            int pos = rb.transform.position.x > -1 && rb.transform.position.x < 1 ? 0 : rb.transform.position.x > 0 ? 1 : -1;
            rb.transform.position = new Vector3(x, height, collision.gameObject.transform.position.z - directionMod * 0.8f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            int min = -1 - directionMod * (int)pos;
            int max = 2 - directionMod * (int)pos;
            int xMod = Random.Range(min, max);

            //rb.AddForce(new Vector3(directionMod * xMod * 100, 140, -directionMod * 4 * 200));
			//rb.velocity = new Vector3 (-2.0f, 2.6f, -15.9f);
			//rb.velocity.x = directionMod * xMod * 2.0f;
			//rb.velocity.y = 2.6f;
			//rb.velocity.z = -directionMod * 15.9f;

			rb.velocity = new Vector3 (directionMod * xMod * 2.0f, 2.6f, -directionMod * 15.9f);
        }
    }


	private void OnCollisionExit(Collision collision)
	{
		Debug.Log (rb.velocity);
	}
		
}
