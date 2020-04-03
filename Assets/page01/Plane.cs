using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
	enum State {
		ROTATE,
		FLY,
		IDLE
	}

	State m_State;
	Transform m_Destination;
	Transform m_DepartPoint;

	const float SPEED_ROTATE = 90;  // degree
	const float SPEED_FLY = 60;

	// Use this for initialization
	void Start () {
		m_State = State.FLY;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_State) {
		case State.ROTATE:
			Rotate ();
			break;
		case State.FLY:
			Fly ();
			break;
		default:
			break;
		}
	}

	public void AimTo (GraphNode start, GraphNode destination) {
		transform.position = start.transform.position;
		transform.rotation = Quaternion.FromToRotation (Vector3.right, destination.transform.position - start.transform.position);
		m_Destination = destination.transform;
		m_DepartPoint = start.transform;
		m_State = State.FLY;
	}

	public void SetNewFlight (GraphNode destination) {
		m_DepartPoint = m_Destination;  // old way-point as departing station
		m_Destination = destination.transform;
		//Debug.Log ("[jxd] new destination: " + destination.Label);
		m_State = State.ROTATE;
	}

	public bool Landed () {
		return m_State == State.IDLE;
	}

	void Fly () {
		float distance = Vector3.Distance (transform.position, m_Destination.position);
		if (distance < 0.1f) {
			m_State = State.IDLE;
			return;
		}

		Vector3 move = m_Destination.position - transform.position;
		move.Normalize ();
		float magnitude = Mathf.Min (SPEED_FLY * Time.deltaTime, distance);
		move = move * magnitude;
		transform.Translate (move, Space.World); // what is Space.Self?
	}

	void Rotate () {
		Quaternion rot = Quaternion.FromToRotation (Vector3.right, m_Destination.position - m_DepartPoint.position);
		if (Quaternion.Angle(transform.rotation, rot) < 0.1f) { // Quaternion operator "==" is buggy!
			m_State = State.FLY;
			return;
		}
		float step = SPEED_ROTATE * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards (transform.rotation, rot, step);
	}
}
