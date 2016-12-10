using UnityEngine;
using System.Collections;

public class SpringExample : MonoBehaviour {

    [Tooltip("The origin of the hook")]
    public Vector3 m_hookOrigin;
    [Tooltip("Will this spring be a rope")]
    public bool m_Rope = true;

    private bool m_playerConnected = false;
    private SpringJoint m_springJoint;
    private Rigidbody m_hook;

	void Start () {
        // Get all references
        m_springJoint = GetComponent<SpringJoint>();
        m_hook = m_springJoint.connectedBody;
        m_hookOrigin = m_hook.transform.position;
	}

    void OnTriggerEnter(Collider col)
    {
        // If the collider that has entered has a rigidbody
        Rigidbody colliderRB = col.gameObject.GetComponent<Rigidbody>();
        if (colliderRB)
        {
            // Replace the hook with the new rigidbody
            GameObject hook = m_springJoint.connectedBody.gameObject;
            Vector3 yOffset = new Vector3(0.0f, 0.6f, 0.0f);
            hook.transform.position = col.gameObject.transform.position + yOffset;
            hook.transform.SetParent(col.gameObject.transform);
            m_springJoint.connectedBody = colliderRB;
        }

        // If the collider that has entered has a player controller
        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null)
        {
            // Let the player know that it is connected
            player.IsConnected = true;
            // Give the player a reference to the spring
            player.m_spring = this;

            StartCoroutine(DelayPickup());

            // If it is not a rope, pull up the player by strengthening the spring
            if (!m_Rope)    
            {
                m_springJoint.spring = 200;
            }
        }
    }

    // Detach the player from the spring
    public void DetachFromSpring()
    {
        // Reconnect the hook
        m_hook.transform.parent = null;
        m_hook.transform.position = m_hookOrigin;
        m_springJoint.connectedBody = m_hook;
        m_playerConnected = false;
    }

    // Is the player attacked to the spring
    public bool IsPlayerAttached()
    {
        return m_playerConnected;
    }

    // Set if the player is connected after a time (used in the spring platform)
    private IEnumerator DelayPickup()
    {
        yield return new WaitForSeconds(2);
        m_playerConnected = true;
    }
}
