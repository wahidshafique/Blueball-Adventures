using UnityEngine;
using System.Collections;

public class MovePlat2 : MonoBehaviour {
    private Transform pointStart;
    public Transform pointInter;
    public Transform pointEnd;

    public bool isAlt = false;
    public float speed = 5;

    private Vector3 directionInter;
    private Vector3 directionFinal;

    private Rigidbody m_rb;
    private bool toggle = true;

    private float origDistInter;
    private float origDistFinal;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        pointStart = this.transform;
        directionInter = (pointInter.position - transform.position).normalized * speed;
        origDistInter = Vector3.Distance(this.transform.position, pointInter.position);

        origDistFinal = Vector3.Distance(this.transform.position, pointEnd.position);
        directionFinal = (pointEnd.position - transform.position).normalized * speed;
    }

    void Update() {
        if (!isAlt)
            movetoPoints(directionInter, pointInter, origDistInter);
        else movetoPoints(directionFinal, pointEnd, origDistFinal);
    }

    void movetoPoints(Vector3 direction, Transform point, float originalDistance) {
        if (Vector3.Distance(this.transform.position, point.position) > 1f && toggle) {
            m_rb.velocity = direction;
        } else {
            m_rb.velocity = new Vector3(0, 0, 0);
            toggle = false;
        }
        if (!toggle) {
            m_rb.velocity = -direction;
            if (Vector3.Distance(this.transform.position, point.position) > originalDistance) {
                m_rb.velocity = new Vector3(0, 0, 0);
                toggle = true;
            }
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player")) {
            isAlt = true;
        }
    }
    void OnCollisionStay(Collision coll) {
        if (coll.gameObject.CompareTag("Player")) {
            coll.rigidbody.velocity = m_rb.velocity;
        }
    }
    void OnCollisionExit(Collision coll) {
        if (coll.gameObject.CompareTag("Player")) {
            coll.rigidbody.velocity = new Vector3(0,0,0);
        }
    }
}
