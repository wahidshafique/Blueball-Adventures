using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public Rigidbody m_other;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate() {
        // if this doesn't work try "B.transform.eulerAngles - A.transform.eulerAngles" below
        Vector3 difference = m_other.transform.eulerAngles - this.transform.eulerAngles;
        Vector3 velocity = difference / Time.fixedDeltaTime;
        m_other.angularVelocity = velocity;

    }
}
