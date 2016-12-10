using UnityEngine;
using System.Collections;

public class SpringExample : MonoBehaviour {

    public Vector3 m_hookOrigin;
    public bool m_Rope = true;
    private bool m_playerConnected = false;
    private SpringJoint m_springJoint;
    private Rigidbody m_hook;
    private float m_originalSpringStrength = 10;
    private Vector3 m_originalConnectedDist;

	void Start () {
        m_springJoint = GetComponent<SpringJoint>();
        m_hook = m_springJoint.connectedBody;
        m_hookOrigin = m_hook.transform.position;
        m_originalConnectedDist = m_springJoint.connectedAnchor;
	}

    void OnTriggerEnter(Collider col)
    {
        //if (!m_playerConnected)
        //{
            Rigidbody colliderRB = col.gameObject.GetComponent<Rigidbody>();
            if (colliderRB)
            {
                GameObject hook = m_springJoint.connectedBody.gameObject;
                Vector3 yOffset = new Vector3(0.0f, 0.6f, 0.0f);
                hook.transform.position = col.gameObject.transform.position + yOffset;
                hook.transform.SetParent(col.gameObject.transform);
                // We become attached to the spring
                // and the old connected body is no longer attached
                m_springJoint.connectedBody = colliderRB;
            }

            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.IsConnected = true;
                player.m_spring = this;
                StartCoroutine(DelayPickup());
                if (!m_Rope)    // If it is not a rope, pull up the player by strengthening the spring
                {
                    m_springJoint.spring = 200;
                }
            }
        //}

    }

    public void DetachFromSpring()
    {
        m_hook.transform.parent = null;
        m_springJoint.spring = m_originalSpringStrength;
        m_hook.transform.position = m_hookOrigin;
        m_springJoint.connectedBody = m_hook;
        m_playerConnected = false;
        m_springJoint.spring = m_originalSpringStrength;
        //m_springJoint.connectedAnchor = m_originalConnectedDist;
    }

    public bool IsPlayerAttached()
    {
        return m_playerConnected;
    }

    private IEnumerator DelayPickup()
    {
        yield return new WaitForSeconds(2);
        m_playerConnected = true;
    }
}
