using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic : MonoBehaviour {

	public static SceneLogic sceneLogic = null;

	public Transform m_ServePoint = null;
	public Transform m_ServePointMySelf = null;
	public float m_BallFlyingTime = 2.0f;

	public float m_SpeedXZ = 0.0f;
	public Vector2 m_DirXZ = new Vector2 ();

	private GameObject m_LandingPoint = null;

	void Awake() {
		sceneLogic = this;
	}
	

	// Use this for initialization
	void Start () {
		m_LandingPoint = Instantiate (Resources.Load ("Prefab/MovingCircle", typeof(GameObject))) as GameObject;
		if (null != m_LandingPoint) {
			m_LandingPoint.transform.position = Vector3.zero;
			m_LandingPoint.transform.rotation = Quaternion.Euler (Vector3.zero);
			m_LandingPoint.SetActive (false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ActiveLandingPoint(Vector3 pos) {
		if (null == m_LandingPoint)
			return;

		if (!m_LandingPoint.activeSelf) {
			m_LandingPoint.SetActive (true);
		}

		for (int i = 0; i < m_LandingPoint.transform.childCount; ++i)
		{
			GameObject child = m_LandingPoint.transform.GetChild(i).gameObject;
			if (null != child)
			{
				ParticleSystem particle = child.GetComponent<ParticleSystem>();
				if (null != particle)
				{
					particle.time = 0.0f;
				}
			}
		}

		m_LandingPoint.transform.position = pos;
	}

	public void DeactiveLandingPoint() {
		if (null != m_LandingPoint) {
			m_LandingPoint.SetActive (false);
		}
	}


	public void ServeBall() {
		GameObject ball = ResourceManager.InstantiateResource("Prefab/Ball", "Ball", typeof(GameObject)) as GameObject;
		ball.transform.position = m_ServePoint.position;
		BallMovementByScript movement = ball.AddComponent<BallMovementByScript> ();
		movement.enabled = true;
		movement.m_Velocity = MoveBallCaculator.CaculateVelocityToHitTargetAtTime (ball.transform.position, m_LandingPoint.transform.position, movement.m_Gravity, m_BallFlyingTime );

	}

	public void ServeBallMyself()
	{
		GameObject ball = ResourceManager.InstantiateResource("Prefab/Ball", "Ball", typeof(GameObject)) as GameObject;
		ball.transform.position = m_ServePointMySelf.position;
		BallMovementByScript movement = ball.AddComponent<BallMovementByScript> ();
		movement.enabled = true;

		movement.m_Velocity.x = m_SpeedXZ * m_DirXZ.x;
		movement.m_Velocity.z = m_SpeedXZ * m_DirXZ.y;
		movement.m_Velocity.y = MoveBallCaculator.CaculateMinVelocityYComponentToReachHeight(Mathf.Abs(ball.transform.position.z - 0), 0.58f, movement.m_Velocity.z, movement.m_Gravity);

	}


}
