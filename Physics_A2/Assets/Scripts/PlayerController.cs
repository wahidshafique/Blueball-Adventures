using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float deathY = -10f;
    public Vector3 m_maxSpeed = new Vector3(2.0f, 6.0f, 2.0f);

    public bool m_canMove = true;
    private Rigidbody m_rb = null;

    private bool m_isConnected = false;
    public bool IsConnected {
        set { m_isConnected = value; }
        get { return m_isConnected; }
    }
    public SpringExample m_spring;

    private bool m_isOnIce = false;
    private bool m_isOnGlue = false;

    // Use this for initialization
    void Start() {
        m_rb = GetComponent<Rigidbody>();
        m_isConnected = false;
    }

    void MovePlayer(float xAxis, float zAxis, bool isJumping) {
        if (m_isConnected) {
            if (isJumping) {
                m_spring.DetachFromSpring();
                m_spring = null;
                m_isConnected = false;
            }
            return;
        }
        
        Vector3 moveVelocity = Vector3.zero;
        Vector3 xMovement = (xAxis * m_maxSpeed.x * transform.right);
        Vector3 zMovement = (zAxis * m_maxSpeed.z * transform.forward);
        moveVelocity = xMovement + zMovement;

        moveVelocity.y = m_rb.velocity.y;

        if (isJumping && (Mathf.Abs(m_rb.velocity.y) < 0.0001f)) {
            moveVelocity.y = m_maxSpeed.y;
        }

        if(m_isOnIce)
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

    // Update is called once per frame
    void Update() {
        if (m_canMove)
        {
            if (transform.position.y > deathY)
            {
                float verticalAxis = Input.GetAxis("Vertical");
                float horizontalAxis = Input.GetAxis("Horizontal");
                bool  isJumping = Input.GetKeyDown(KeyCode.Space);

                MovePlayer(horizontalAxis, verticalAxis, isJumping);
            }
            else
            {
                print("YOU DIED BY FALLING");
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DisableControl"))
        {
            m_canMove = false;
        }

        if (other.CompareTag("EnableControl"))
        {
            m_canMove = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(other.collider.CompareTag("IceFloor"))
        {
            m_isOnIce = true;
        }
        else
        {
            m_isOnIce = false;
        }
    }
}
