using UnityEngine;
using System.Collections;

public class CameraZone : MonoBehaviour {

    private Camera m_camera;
    private CameraManager m_camController;
    private int m_camNum = 0;

	void Start ()
    {
        m_camera = GetComponentInChildren<Camera>();
        m_camController = FindObjectOfType<CameraManager>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        // If the player enters 
        if(other.CompareTag("Player"))
        {
            // Set the camera to the controller
            m_camNum = m_camController.AddCamera(m_camera);
            // Switch to the new camera
            m_camController.SwitchToCamera(m_camNum);
        }
    }
}
