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
        // Set the main camera to camera 0
        m_cameras.Add(m_mainCamera);    
	}

    void OnTriggerEnter(Collider other)
    {
        // Return to the main camera when entering the Main Camera Zone
        if(other.CompareTag("MainCameraZone"))
        {
            SwitchToCamera(0);
        }
    }

    // Add a camera to the list, return the camera number
    public int AddCamera(Camera cam)
    {
        // If not already in the linst
        if (!m_cameras.Contains(cam))
        {
            // Add the new camera
            m_cameras.Add(cam);
            // Return the camera's number
            return m_cameras.Count - 1; 
        }
        else
        {
            // Return the already existing camera index
            return m_cameras.IndexOf(cam);  
        }
    }

    // Switch to the camera number passed in
    public void SwitchToCamera(int cameraNum)
    {
        // As long as the camera number is less than the amount in the list
        if (Mathf.Abs(cameraNum) < m_cameras.Count)
        {
            // Turn off all cameras
            foreach (Camera c in m_cameras)
            {
                c.enabled = false;
            }
            // Enable the camera passed
            m_cameras[Mathf.Abs(cameraNum)].enabled = true;
        }
    }

}
