using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIdPylon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IPylon[] pylons = GetComponentsInChildren<IPylon>();
        foreach (IPylon pylon in pylons)
        {
            Debug.Log("Pilon ID is " + pylon.GetPylonId());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
