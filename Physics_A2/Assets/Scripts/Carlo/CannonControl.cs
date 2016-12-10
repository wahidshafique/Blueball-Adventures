using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {

    [Tooltip("The target reticle")]
    public TargetReticle m_target;
    [Tooltip("The cannon's pivot")]
    public Transform m_cannonPivot;
    [Tooltip("The end of the cannon")]
    public Transform m_cannonEnd;
    [Tooltip("The speed of the cannon's rotation")]
    public float speed;
    [Tooltip("The time the player will reach the target")]
    public float m_timeToTarget = 3.0f;

    private bool m_hasPlayer = false;
    private PlayerController m_player;

	void Update () {
        // If the player is in the cannon
        if(m_hasPlayer)
        {
            // Grab the input
            float v = Input.GetAxis("Vertical");
            // Rotate the camera
            m_cannonPivot.Rotate(Vector3.right, v * speed * Time.deltaTime);
            // Allow the target reticle to be controlled
            m_target.m_canControl = true;
            // Launch
            if(Input.GetButtonDown("Jump"))
            {
                ObjectLauncher launcher = m_player.GetComponent<ObjectLauncher>();

                // Set player time to target
                launcher.SetTime(m_timeToTarget);
                // Detach player
                m_player.transform.parent = null;
                // Set player target
                launcher.SetTarget(m_target.transform);
                // Disable kinematic on player
                m_player.GetComponent<Rigidbody>().isKinematic = false;
                // Launch the player
                launcher.LaunchProjectile();

                // Player no longer can control the cannon
                m_hasPlayer = false;
                // Stop the reticle control
                m_target.m_canControl = false;
            }
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        // When the player the enters the cannon
        if (!m_hasPlayer)
        {
            if (other.CompareTag("Player"))
            {
                m_player = other.GetComponent<PlayerController>();

                // Set the player's position and parent the player to the cannon
                m_player.transform.parent = m_cannonEnd;
                m_player.transform.position = m_cannonEnd.position;
                // Disable player control
                m_player.m_canMove = false;
                m_player.GetComponent<Rigidbody>().isKinematic = true;
                // Allow player to control the cannon
                m_hasPlayer = true;
            }
        }
    }
}
