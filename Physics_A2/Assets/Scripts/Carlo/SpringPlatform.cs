using UnityEngine;
using System.Collections;

public class SpringPlatform : MonoBehaviour {

    public SpringExample m_spring;
    public Transform m_targetPosition;
    public float m_platformSpeed = 10.0f;

    private Vector3 m_direction;
    private Vector3 m_initialPosition;

	void Start () 
    {
        m_initialPosition = transform.position;
        m_direction = m_targetPosition.position - m_initialPosition;
	}
	
	void Update () 
    {
	    if(m_spring.IsPlayerAttached())
        {
            Debug.Log(Vector3.Distance(transform.position, m_targetPosition.position));
            if (Vector3.Distance(transform.position, m_targetPosition.position) > 0.5f)
            {
                transform.Translate(m_direction * m_platformSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, m_initialPosition) > 0.5f)
            {
                transform.Translate(-m_direction * m_platformSpeed * Time.deltaTime);
            }
        }
	}
}
