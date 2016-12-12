using UnityEngine;
using System.Collections;

public class MovePlat2 : MonoBehaviour {
    private Transform pointStart;
    public Transform pointInter;
    public Transform pointEnd;

    public float speed = 5;

    private Vector3 directionInter;
    private Vector3 directionFinal;

    private Rigidbody m_rb;
    private bool toggle = true;

    private float origDist;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        pointStart = this.transform;
        directionInter = (pointInter.position - transform.position).normalized * speed;
        origDist = Vector3.Distance(this.transform.position, pointInter.position);
    }

    void Update() {
        print(Vector3.Distance(this.transform.position, pointInter.position));
        if (Vector3.Distance(this.transform.position, pointInter.position) > 0.1f && toggle) {
            m_rb.velocity = directionInter;
        } else {
            m_rb.velocity = new Vector3(0, 0, 0);
            toggle = false;
        }
        if (!toggle) {
            m_rb.velocity = -directionInter;
            if (Vector3.Distance(this.transform.position, pointInter.position) > origDist) {
                m_rb.velocity = new Vector3(0, 0, 0);
                toggle = true;
            }
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player")) {

        }
    }
}
