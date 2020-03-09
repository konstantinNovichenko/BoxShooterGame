using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicController : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {

        Debug.Log("Horizontal Input = " + Input.GetAxis("Horizontal"));
        
		
	}
}
