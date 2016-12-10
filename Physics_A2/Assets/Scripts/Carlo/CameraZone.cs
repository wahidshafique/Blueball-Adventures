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
        if(other.CompareTag("Player"))
        {
            m_camNum = m_camController.AddCamera(m_camera);
            m_camController.SwitchToCamera(m_camNum);
        }
    }
}
