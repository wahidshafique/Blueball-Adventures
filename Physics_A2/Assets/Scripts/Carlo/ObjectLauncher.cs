using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ObjectLauncher : MonoBehaviour {

    [SerializeField, Tooltip("The time it takes to go to the target.")]
    private float m_time;

    [SerializeField, Tooltip("The reference object that we want to land on top of.")]
    private Transform m_referenceObject;

    [SerializeField, Tooltip("Should the projectile launch on Start.")]
    private bool m_launchOnStart = false;

    // Reference to rigidbody.
    private Rigidbody m_rb = null;
    private Vector3 m_force = Vector3.zero;
    private Vector3 m_desiredDisplacement = Vector3.zero;

    void Start()
    {
        // Get a reference to the rigidbody
        m_rb = GetComponent<Rigidbody>();

        // Freeze all rotation
        m_rb.freezeRotation = true;

        // Divide the time in half to use in the formulas
        m_time = m_time * 0.5f;

        // Launch the projectile on start if needed
        if (m_launchOnStart)
        {
            LaunchProjectile();
        }
    }

    public void LaunchProjectile()
    {
        // Reset the velocity to 0
        m_rb.velocity = Vector3.zero;

        // Getting the start and end position of the projectile
        Vector3 startPosition = transform.position;
        Vector3 endPosition = m_referenceObject.position;

        // Adjust the positions, taking into account the heights of the projectile and target
        startPosition.y = GetBottom(transform);
        endPosition.y = GetTop(m_referenceObject);

        // Calculate the desired displacement
        m_desiredDisplacement = endPosition - startPosition;

        // Calculate the vertical direction force
        m_force.y = CalculateYImpulse(m_desiredDisplacement.y, m_time);

        // Calculate the horizontal force
        m_force.x = (m_desiredDisplacement.x / m_time) * m_rb.mass;
        m_force.z = (m_desiredDisplacement.z / m_time) * m_rb.mass;

        // Add the force to the rigidbody
        m_rb.AddForce(m_force / Time.fixedDeltaTime);

        // Reset the force to 0
        m_force = Vector3.zero;
    }

    public void SetTarget(Transform target)
    {
        m_referenceObject = target;
    }

    private float CalculateYImpulse(float displacement, float time)
    {
        // Kinematic formula
        float velocity = (displacement - (0.5f * (Physics.gravity.y) * (time * time))) / time;

        return velocity * m_rb.mass;
    }

    private float GetTop(Transform refObject)
    {
        return refObject.position.y + (refObject.localScale.y * 0.5f);
    }

    private float GetBottom(Transform refObject)
    {
        return refObject.position.y - (refObject.localScale.y * 0.5f);
    }
}