using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {

    private List<Camera> m_cameras;
    private Camera m_mainCamera;
	
	void Start()
    {
        m_mainCamera = Camera.main;
        m_cameras = new List<Camera>();
        m_cameras.Add(m_mainCamera);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCameraZone"))
        {
            SwitchToCamera(0);
        }
    }

    public int AddCamera(Camera cam)
    {
        if (!m_cameras.Contains(cam))
        {
            m_cameras.Add(cam);
            return m_cameras.Count - 1; // Return the camera's number
        }
        else
        {
            return m_cameras.IndexOf(cam);
        }
    }

    public void SwitchToCamera(int cameraNum)
    {
        foreach(Camera c in m_cameras)
        {
            c.enabled = false;
        }
        m_cameras[cameraNum].enabled = true;
    }

}
