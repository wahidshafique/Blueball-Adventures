using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float timeToDestruction = 3.0f;

	void Start () 
    {
        Destroy(this.gameObject, timeToDestruction);
	}

}
