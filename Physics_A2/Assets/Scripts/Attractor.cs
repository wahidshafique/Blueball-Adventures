using UnityEngine;
using System.Collections;

public class Attractor : MonoBehaviour {
    public float gravity = -10;
    public float torqueModifier = 5;
    private Rigidbody m_rb;

    void Start() {
        m_rb = this.GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        m_rb.AddTorque(transform.up * torqueModifier);
    }
    public void Attract(Transform body) {
        //dir of player from center
        Vector3 gravUp = (body.position - transform.position).normalized;
        //dir of where the player is facing
        Vector3 bodyUp = body.up;
        //always go to planet 
        body.GetComponent<Rigidbody>().AddForce(gravUp * gravity);
        
        Quaternion targetRot = Quaternion.FromToRotation(bodyUp, gravUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRot, 40 * Time.deltaTime);
    }
}
