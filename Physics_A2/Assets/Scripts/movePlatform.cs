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
    public float maxDistanceX = 10;

    void Start() {
        m_rb = this.GetComponent<Rigidbody>();
        startPos = transform.position;
        print(startPos);
        maxDistanceX = this.transform.position.x + maxDistanceX;
    }

    void Update() {
        print(distTravelled);
        if (distTravelled >= maxDistanceX && switcher) {
            modifier = -modifier;
            switcher = false;
        } else if (distTravelled <= startPos.x) {
            switcher = true;
        }

        if (switcher) {
            distTravelled += Vector3.Distance(transform.position, lastPos);
        } else {
            distTravelled -= Vector3.Distance(transform.position, startPos);
        }
        lastPos = transform.position;
    }

    void FixedUpdate() {
        m_rb.velocity = new Vector3(modifier * 10, 0, 0);
    }

    void OnCollisionEnter(Collision coll) {
    }
}
