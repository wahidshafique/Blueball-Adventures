using UnityEngine;
using System.Collections;

public class SpringExample : MonoBehaviour {

    private SpringJoint m_springJoint;
    private Rigidbody m_hook;
    public Vector3 m_hookOrigin;

    void OnTriggerEnter(Collider col)
    {
        Rigidbody colliderRB = col.gameObject.GetComponent<Rigidbody>();
        if(colliderRB)
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
        if(player != null)
        {
            player.IsConnected = true;
            player.m_spring = this;
        }
        
    }

	// Use this for initialization
	void Start () {
        m_springJoint = GetComponent<SpringJoint>();
        m_hook = m_springJoint.connectedBody;
        m_hookOrigin = m_hook.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DetachFromSpring()
    {
        m_hook.transform.parent = null;
        m_hook.transform.position = m_hookOrigin;
        m_springJoint.connectedBody = m_hook;
    }
}
