using UnityEngine;
using System.Collections;

public class sinkingPlatform : MonoBehaviour {
    private Rigidbody m_rb;
    private float finalValue = -0.1f;
    public bool triggered;
	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (triggered) {
            m_rb.AddRelativeForce(new Vector3(0.0f, finalValue, 0.0f), ForceMode.Acceleration);
        }
    }

    void OnColliderEnter(Collision coll) {
        triggered = true;
        
    }
}
