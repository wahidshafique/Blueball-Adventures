using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {

    public TargetReticle m_target;
    public Transform m_cannonPivot;
    public Transform m_cannonEnd;
    public float speed;
    public float m_timeToTarget = 3.0f;
    private bool m_hasPlayer = false;
    private PlayerController m_player;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(m_hasPlayer)
        {
            float v = Input.GetAxis("Vertical");
            m_cannonPivot.Rotate(Vector3.right, v * speed * Time.deltaTime);

            m_target.m_canControl = true;

            if(Input.GetButtonDown("Jump"))
            {
                ObjectLauncher launcher = m_player.GetComponent<ObjectLauncher>();
                launcher.SetTime(m_timeToTarget);
                m_player.transform.parent = null;
                launcher.SetTarget(m_target.transform);
                m_player.GetComponent<Rigidbody>().isKinematic = false;
                launcher.LaunchProjectile();
                m_hasPlayer = false;
                // ReEnable player control
                //m_player.m_canMove = true;
                m_target.m_canControl = false;
            }
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (!m_hasPlayer)
        {
            if (other.CompareTag("Player"))
            {
                m_player = other.GetComponent<PlayerController>();
                m_player.transform.parent = m_cannonEnd;
                m_player.transform.position = m_cannonEnd.position;
                // Disable player control
                m_player.m_canMove = false;
                m_player.GetComponent<Rigidbody>().isKinematic = true;
                m_hasPlayer = true;
            }
        }
    }
}
