using UnityEngine;
using System.Collections;

public class TargetRamp : MonoBehaviour
{
    public GameObject pTargetLocation;
    public GameObject pRampStartPoint;
    public GameObject pRampEndPoint;
    public float pTargetAcceleration = 20f;
    public string pPlayerTag = "Player";

    private bool mReady;
    private bool mArmed;
    private bool mActivated;

    private Rigidbody mPlayer;
    private Vector3 mContactPoint;

    void Awake()
    {
        mReady = false;
        mArmed = false;
        mActivated = false;
    }

	// Use this for initialization
	void Start ()
    {
        if (pTargetLocation
            && pRampStartPoint
            && pRampEndPoint)
        {
            mReady = true;
        }
        else
        {
            Debug.LogWarning("WARNING - Please ensure all required GameObjects have been assigned");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        if (!mReady)
        {
            return;
        }

        if (mActivated)
        {
            NudgeTarget();
        }

        if (mArmed)
        {
            mArmed = false;
            LaunchTarget();
            mPlayer = null;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(pPlayerTag))
        {
            return;
        }

        mPlayer = other.gameObject.GetComponent<Rigidbody>();

        if (!mPlayer)
        {
            return;
        }

        mContactPoint = other.contacts[0].point;
        mActivated = true;
    }

    void OnCollisionStay(Collision other)
    {
        mContactPoint = other.contacts[0].point;
    }

    void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag(pPlayerTag))
        {
            return;
        }

        if (!mPlayer)
        {
            return;
        }

        mActivated = false;
        mArmed = true;
    }

    private void NudgeTarget()
    {
        Vector3 rampVector = pRampEndPoint.transform.position - pRampStartPoint.transform.position;
        float targetDistanceToEnd = (pRampEndPoint.transform.position - mContactPoint).magnitude;
        float rampStrength = pTargetAcceleration * mPlayer.mass;
        float strengthDampener = Mathf.Min((rampVector.magnitude - targetDistanceToEnd) / rampVector.magnitude, 0.1f);
        float rampMagnitude = rampVector.magnitude;
        rampVector.y = 0f;
        Vector3 rampDirection = Vector3.Project(rampVector, this.transform.right).normalized;
        Vector3 finalVector = (rampDirection * rampStrength * strengthDampener) / Time.fixedDeltaTime;

        mPlayer.AddForce(finalVector);
    }

    private void LaunchTarget()
    {
        Vector3 launchPosition = mContactPoint - mPlayer.transform.position;

        if (launchPosition.magnitude > this.transform.localScale.z)
        {
            return;
        }

        Vector3 launchVector = pTargetLocation.transform.position - mPlayer.transform.position;
        Vector3 launchVelocity = mPlayer.velocity;
        float accelerationDueToGravity = Physics.gravity.magnitude;
        float freeFallTime = launchVelocity.y / accelerationDueToGravity;
        float verticalRampBuffer = launchVector.y - (launchVelocity.y * freeFallTime
                                  + accelerationDueToGravity * freeFallTime * freeFallTime / 2f);
        float timeAfterBuffer = Mathf.Sqrt((2f * verticalRampBuffer) / accelerationDueToGravity);

        float additionalVelocityY = 0f;
        if (verticalRampBuffer > 0f)
        {
            additionalVelocityY = timeAfterBuffer * accelerationDueToGravity;
        }

        freeFallTime += timeAfterBuffer;

        float additionalVelocityX = (launchVector.x / freeFallTime) - launchVelocity.x;
        float additionalVelocityZ = (launchVector.z / freeFallTime) - launchVelocity.z;

        Vector3 additionalMomentum = Vector3.one * mPlayer.mass;
        additionalMomentum.y *= additionalVelocityY;
        additionalMomentum.x *= additionalVelocityX;
        additionalMomentum.z *= additionalVelocityZ;

        Vector3 additionalImpulse = additionalMomentum / Time.fixedDeltaTime;
        mPlayer.AddForce(additionalImpulse.normalized * additionalImpulse.magnitude);
    }
}
