using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallCaculator {
	
	/// <summary>
	/// 根据水平角、垂直角、速度标量，计算速度向量
	/// </summary>
	public static void CaculateVelocity(ref Vector3 velocity, float horizentalAngle, float verticalAngle, float speed) {
		float horizentalRadian = horizentalAngle * Mathf.Deg2Rad;
		float verticalRadian = verticalAngle * Mathf.Deg2Rad;

		velocity.x = speed * Mathf.Sin (verticalRadian) * Mathf.Cos (horizentalRadian);
		velocity.y = speed * Mathf.Cos (verticalRadian);
		velocity.z = speed * Mathf.Sin (verticalRadian) * Mathf.Sin (horizentalRadian);	
	}

	/// <summary>
	/// 根据重力加速度和时间间隔，更新球的位置和移动速度
	/// </summary>
	public static void UpdateBallMovement(ref Vector3 currentPosition, ref Vector3 currentVelocity, float gravityNegative, float deltaTime) {
		currentPosition += currentVelocity * deltaTime;
		currentVelocity.y += gravityNegative * deltaTime;
	}

	/// <summary>
	/// 计算球反弹之后的移动速度
	/// </summary>
	/// <param name="currentVelocity">Current velocity.</param>
	public static void BounceBall(ref Vector3 currentVelocity) {
		currentVelocity.y = -currentVelocity.y;
	}

	/// <summary>
	/// 根据起点、终点、重力加速度和运动时间，计算球的初始速度
	/// </summary>
	public static Vector3 CaculateVelocityToHitTargetAtTime(Vector3 startPosition, Vector3 targetPosition, float gravityNegative, float timeToTargetPosition) {
		if (timeToTargetPosition <= 0.0f)
		{
			Debug.LogError("CaculateStartVelocity called with invalid timeToTargetPosition");
		}

		// calculate forward speed
		Vector3 startToEndFlat = targetPosition - startPosition;
		startToEndFlat.y       = 0.0f;
		float flatDistance     = startToEndFlat.magnitude;
		float forwardSpeed     = flatDistance / timeToTargetPosition;

		// calculate vertical speed
		float heightDiff       = targetPosition.y - startPosition.y;
		float upSpeed          = (heightDiff - (0.5f * gravityNegative * timeToTargetPosition * timeToTargetPosition)) / timeToTargetPosition;

		// initialize velocity
		Vector3 velocity       = startToEndFlat.normalized * forwardSpeed;
		velocity.y             = upSpeed;

		return velocity;
	}


	public static float CaculateMinVelocityYComponentToReachHeight(float distanceOffset, float heightOffset, float speed, float gravityNegative)
	{
		//return 10.0f;
		float timeDistance = distanceOffset / speed;
		return (heightOffset - 0.5f * gravityNegative * timeDistance * timeDistance) / timeDistance;

	}
    


}
