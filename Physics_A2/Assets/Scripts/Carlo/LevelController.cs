using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public Transform m_startTarget;
    private PlayerController m_player;
    private bool m_gameStarted = false;

	void Start () 
    {
        m_player = FindObjectOfType<PlayerController>();
	}

	void Update ()
    {
        if (!m_gameStarted)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_player.GetComponent<ObjectLauncher>().SetTarget(m_startTarget);
                m_player.GetComponent<ObjectLauncher>().LaunchProjectile();
                //m_player.m_canMove = true;
                m_gameStarted = true;
            }
        }
	}
}
