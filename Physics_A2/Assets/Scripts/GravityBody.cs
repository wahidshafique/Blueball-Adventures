using UnityEngine;
using System.Collections;

public class GravityBody : MonoBehaviour {
    public Attractor attractor;
    public bool isOnAttactor = false;
    private bool initials;
    private Transform m_transform;
    private Rigidbody m_rb; 

    void Start() {
        m_rb = this.GetComponent<Rigidbody>();
        m_transform = this.transform;
    }

	void Update () {
        if (isOnAttactor) {
            print("is on attractor");
            attractor.Attract(m_transform);
        } else {
            print("is off");
        }
	}
    void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Attractor")) {
            print("on rotating object");
            toggleGravity(false);
            isOnAttactor = true;
        }
    }

    public void toggleGravity(bool toggle) {
        if (!toggle) {
            m_rb.useGravity = false;
        } else {
            m_rb.useGravity = true;
        }

    }
}
