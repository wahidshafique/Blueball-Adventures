using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class Rotator : MonoBehaviour {
    public Rigidbody m_other_rb;
    private Rigidbody m_rb;
	// Use this for initialization
	void Start () {
        m_rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate() {
        m_rb.AddTorque(-50, 0, 0);
    }
}
