using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    [Tooltip("The platform the player will reach after being launched")]
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
            // Press Jump to start the game
            if (Input.GetButtonDown("Jump"))
            {
                // Set the target and launch the player
                m_player.GetComponent<ObjectLauncher>().SetTarget(m_startTarget);
                m_player.GetComponent<ObjectLauncher>().LaunchProjectile();
                m_gameStarted = true;
            }
        }
	}
}
