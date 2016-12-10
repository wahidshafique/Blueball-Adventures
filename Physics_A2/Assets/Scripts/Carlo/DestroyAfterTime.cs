using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [Tooltip("Time after which the object is destroyed")]
    public float timeToDestruction = 3.0f;

	void Start () 
    {
        Destroy(this.gameObject, timeToDestruction);
	}

}
