using UnityEngine;
using System.Collections;

public class SpringPlatform : MonoBehaviour {

    [Tooltip("The spring")]
    public SpringExample m_spring;
    [Tooltip("The final posiiton of the platform")]
    public Transform m_targetPosition;
    [Tooltip("The speed of the platform")]
    public float m_platformSpeed = 10.0f;

    private Vector3 m_direction;
    private Vector3 m_initialPosition;

	void Start () 
    {
        // Set the initial position
        m_initialPosition = transform.position;
        // Set the direction to move in
        m_direction = m_targetPosition.position - m_initialPosition;
	}
	
	void Update () 
    {
	    if(m_spring.IsPlayerAttached())
        {
            // If the player is attached and not at the target position 
            if (Vector3.Distance(transform.position, m_targetPosition.position) > 0.5f)
            {
                // Move
                transform.Translate(m_direction * m_platformSpeed * Time.deltaTime);
            }
        }
        else
        {
            // If the player is NOT attached and not at the initial position 
            if (Vector3.Distance(transform.position, m_initialPosition) > 0.5f)
            {
                // Move
                transform.Translate(-m_direction * m_platformSpeed * Time.deltaTime);
            }
        }
	}
}
