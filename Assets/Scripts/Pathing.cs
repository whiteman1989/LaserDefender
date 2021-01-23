using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed;
    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        transform.position = waypoints[waypointIndex].position;
	}
	
	// Update is called once per frame
	void Update () {
        if (waypointIndex <= waypoints.Count)
        {

        }
	}
}
