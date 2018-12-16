using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAgent : MonoBehaviour {

    public abstract void ResetPath();
    public abstract void SetSpeed(float speed);
    public abstract float GetRemainingDistance();
    public abstract void SetDestination(Vector3 destination);
    public abstract float GetAgentLookAngleDiff();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
