using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class movePlatform : MonoBehaviour {
    Rigidbody m_rb;
    Vector3 startPos;
    Vector3 lastPos;
    bool switcher = true;
    float modifier = 1;
    float distTravelled = 0;
    float maxDistance = 10;

    void Start() {
        m_rb = this.GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void Update() {
        if (distTravelled > maxDistance && switcher) {
            modifier = -modifier;
            switcher = false;
        } else if (distTravelled < startPos.x) {
            switcher = true;
        }

        if (switcher) {
            distTravelled += Vector3.Distance(transform.position, lastPos);
        } else {
            distTravelled -= Vector3.Distance(transform.position, lastPos);
        }
        lastPos = transform.position;
    }

    void FixedUpdate() {
        m_rb.velocity = new Vector3(modifier * 10, 0, 0);
    }

    void OnCollisionEnter(Collision coll) {
    }
}
