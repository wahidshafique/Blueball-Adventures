using UnityEngine;

public class Lift : MonoBehaviour
{
    public bool pSingleActivation = true;
    public string pPlayerTag = "Player";
    public float pTargetHeight = 10f;

    private bool mArmed;
    private bool mActivated;

    private Rigidbody mPlayer;

    void Awake()
    {
        mArmed = true;
        mActivated = false;
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        if (mArmed && mActivated && mPlayer)
        {
            LaunchTarget();
            mArmed = false;
            mActivated = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(pPlayerTag))
        {
            return;
        }

        mPlayer = other.gameObject.GetComponent<Rigidbody>();

        if (!mPlayer)
        {
            return;
        }

        mActivated = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(pPlayerTag))
        {
            return;
        }

        if (!mPlayer)
        {
            return;
        }

        mPlayer = null;

        if (pSingleActivation)
        {
            return;
        }

        mArmed = true;
    }

    private void LaunchTarget()
    {
        float innateMomentum = -mPlayer.velocity.y * mPlayer.mass;
        float accelerationDueToGravity = Physics.gravity.magnitude;
        float freeFallTime = Mathf.Sqrt((2f * pTargetHeight) / accelerationDueToGravity);
        float initialVelocity = accelerationDueToGravity * freeFallTime;
        float initialMomentum = initialVelocity * mPlayer.mass;

        float targetImpulse = (initialMomentum + innateMomentum) / Time.fixedDeltaTime;
        mPlayer.AddForce(Vector3.up * targetImpulse);
    }
}
