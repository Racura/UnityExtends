using UnityEngine;
using System.Collections;

public static partial class ExtendsMathf {

	public static bool InsideVectors (Vector2 dir, Vector2 a, Vector2 b)
	{
		int i = 0;
		
		if(dir.x * a.y - dir.y * a.x < 0)
			++i;

		if(dir.y * b.x - dir.x * b.y < 0)
			++i;
		
		if(i == 1){
			if(b.x * a.y - b.y * a.x > 0)
				++i;
		}

		return i > 1;
	}

	public static Vector2 Vector2FromDegrees (float angle) {
		return new Vector2(
			Mathf.Sin(Mathf.Deg2Rad * angle),
			Mathf.Cos(Mathf.Deg2Rad * angle)
		);
	}

	public static float DeltaAngleSign (float angle1, float angle2) {
		var delta = angle2 - angle1;
		while(delta > 180 || delta < -180)
			delta = delta % 360;

		return delta;
	}

	public static float DeltaAngleSign (Vector2 vector1, Vector2 vector2) {
		return Vector2.Angle(vector1, vector2) * Mathf.Sign(vector2.y * vector1.x - vector2.x * vector1.y);
	}



	public static float DistanceToStopped(Vector2 velocity, float acceleration){
		return (velocity.sqrMagnitude * 0.5f)  / acceleration;
	}

	public static float DistanceToStopped(float velocity, float acceleration){
		return (velocity * velocity * 0.5f) / acceleration;
	}

	public static float DistanceToVelocity(Vector2 currentVelocity, Vector2 wantedVelocity, float acceleration){

		var difVel = wantedVelocity - currentVelocity;
		return DistanceToStopped(difVel, acceleration);
	}

	public static float DistanceToVelocity(float currentVelocity, float wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return DistanceToStopped(difVel, acceleration);
	}

	public static float TimeToVelocity(Vector2 currentVelocity, Vector2 wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return (difVel.magnitude) / acceleration;
	}

	public static float TimeToVelocity(float currentVelocity, float wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return (difVel) / acceleration;
	}

	public static Vector2 GetHermitePoint (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float tt = t * t;
		float ttt = tt * t;

		float d1 = 2 * ttt + 1 - 3 * tt;         // calculate basis function 1
		float d2 = 3 * tt -2 * ttt;              // calculate basis function 2
		float d3 = ttt - 2 * tt + t;     	     // calculate basis function 3
		float d4 = ttt -  tt;         		     // calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermitePoint (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float tt = t * t;
		float ttt = tt * t;

		float d1 = 2 * ttt + 1 - 3 * tt;         // calculate basis function 1
		float d2 = 3 * tt -2 * ttt;              // calculate basis function 2
		float d3 = ttt - 2 * tt + t;     	     // calculate basis function 3
		float d4 = ttt -  tt;         		     // calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermiteTangent (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float tt = t * t;

		float d1 = 6 * tt - 6 * t;         	// calculate basis function 1
		float d2 = 6 * t -6 * tt;           // calculate basis function 2
		float d3 = 3 * tt - 4 * t + 1;     	// calculate basis function 3
		float d4 = 3 * tt - 2 * t;      	// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector2 GetHermiteTangent (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float tt = t * t;

		float d1 = 6 * tt - 6 * t;         	// calculate basis function 1
		float d2 = 6 * t -6 * tt;           // calculate basis function 2
		float d3 = 3 * tt - 4 * t + 1;     	// calculate basis function 3
		float d4 = 3 * tt - 2 * t;      	// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector2 GetHermiteTangentTangent (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float d1 = 12 * t - 6;         		// calculate basis function 1
		float d2 = 6 - 12 * t;           	// calculate basis function 2
		float d3 = 6 * t - 4;     			// calculate basis function 3
		float d4 = 6 * t - 2;      			// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermiteTangentTangent (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float d1 = 12 * t - 6;         		// calculate basis function 1
		float d2 = 6 - 12 * t;           	// calculate basis function 2
		float d3 = 6 * t - 4;     			// calculate basis function 3
		float d4 = 6 * t - 2;      			// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}
}