using UnityEngine;
using System.Collections;

public class ProjectileTrajectory : MonoBehaviour {


    public GameObject point;
    public Transform target;
    public float interval;

	void Start () 
    {
        InvokeRepeating("SpawnPoint", 0.0f, interval);
	}

	void SpawnPoint()
    {
        GameObject newPoint = Instantiate(point, transform.position, transform.rotation) as GameObject;
        newPoint.GetComponent<ObjectLauncher>().SetTarget(target);
    }

}
