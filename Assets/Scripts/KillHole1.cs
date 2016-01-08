using UnityEngine;
using System.Collections;

public class KillHole1 : MonoBehaviour {
	public Transform[]patrolPoints;
	public float moveSpeed;
	private int currentPoint;
	private int countPoints;

	void Start () {
		if (patrolPoints.Length > 0) {
			transform.position = patrolPoints [0].position;
			currentPoint = 0;
			countPoints = 0;
			Debug.Log (moveSpeed);
		}
	}

	void Update () {

		if (patrolPoints.Length > 0) {
			if (transform.position == patrolPoints [currentPoint].position) {
				currentPoint++;

			}
			if (currentPoint % 3 == 0) {
				currentPoint = Random.Range (0, patrolPoints.Length);
			}
			if (currentPoint >= patrolPoints.Length) {
				currentPoint = 0;

			}

			transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime);
		}

	}
}
