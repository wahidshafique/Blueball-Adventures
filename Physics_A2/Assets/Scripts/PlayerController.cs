using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Tooltip("The y threshold for player death")]
    public float deathY = -10f;
    [Tooltip("The max speed in all direction")]
    public Vector3 m_maxSpeed = new Vector3(2.0f, 6.0f, 2.0f);
    [Tooltip("Can the player be controlled")]
    public bool m_canMove = true;
    [Tooltip("The spring that the player is attached to ")]
    public SpringExample m_spring;

    private Rigidbody m_rb = null;
    private bool m_isConnected = false;
    public bool IsConnected {
        set { m_isConnected = value; }
        get { return m_isConnected; }
    }
    private bool m_isOnIce = false;
    private bool m_isOnGlue = false;
    private PlayerStateManagerript m_playerStateManager;

    void Start() {
        m_playerStateManager = FindObjectOfType<PlayerStateManagerript>();
        m_rb = GetComponent<Rigidbody>();
        m_isConnected = false;
    }

    void Update() {
        if (m_canMove)
        {
            // If not dead
            if (transform.position.y > deathY)
            {
                // Grab Input
                float verticalAxis = Input.GetAxis("Vertical");
                float horizontalAxis = Input.GetAxis("Horizontal");
                bool  isJumping = Input.GetKeyDown(KeyCode.Space);

                // Move the player
                MovePlayer(horizontalAxis, verticalAxis, isJumping);
            }
            else
            {
                m_playerStateManager.SetPlayerState(EPlayerState.Death);
                // You DEAD
                print("YOU DIED BY FALLING");
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Enter triggers that enable or disable controls
        if(other.CompareTag("DisableControl"))
        {
            m_canMove = false;
        }

        if (other.CompareTag("EnableControl"))
        {
            m_canMove = true;
        }

        if(other.CompareTag("WinPlatform"))
        {
            m_playerStateManager.SetPlayerState(EPlayerState.Win);
        }
    }

    void OnCollisionStay(Collision other)
    {
        // Check for the what surface the player in on
        if(other.collider.CompareTag("IceFloor"))
        {
            m_isOnIce = true;
        }
        else
        {
            m_isOnIce = false;
        }

        if (other.collider.CompareTag("GlueFloor"))
        {
            m_isOnGlue = true;
        }
        else
        {
            m_isOnGlue = false;
        }
    }

    // Moves the player
    private void MovePlayer(float xAxis, float zAxis, bool isJumping)
    {
        // If connected to a spring only allow jumping off of the spring
        if (m_isConnected)
        {
            if (isJumping)
            {
                m_spring.DetachFromSpring();
                m_spring = null;
                m_isConnected = false;
            }
            return;
        }

        // Calculate the movement direction
        Vector3 moveVelocity = Vector3.zero;
        Vector3 xMovement = (xAxis * m_maxSpeed.x * transform.right);
        Vector3 zMovement = (zAxis * m_maxSpeed.z * transform.forward);
        moveVelocity = xMovement + zMovement;
        moveVelocity.y = m_rb.velocity.y;

        // Set Jumping
        if (isJumping && (Mathf.Abs(m_rb.velocity.y) < 0.0001f))
        {
            moveVelocity.y = m_maxSpeed.y;
        }

        // Take into account different surfaces
        if (m_isOnIce)
        {
            // this needs to be here so that the player will keep sliding
            m_rb.AddForce(moveVelocity, ForceMode.Acceleration);
        }
        /*else if(m_isOnGlue)
        {
            // Take into account the friction
        }*/
        else
        {
            m_rb.velocity = moveVelocity;
        }

    }
}
