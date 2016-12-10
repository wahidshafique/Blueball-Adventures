using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum EPlayerState
{
    Normal,
    Win, 
    Death
}

public class PlayerStateManagerript : MonoBehaviour {

    [Tooltip("The text box that will display messages")]
    public Text m_textBox;
    [Tooltip("The index of the main menu")]
    public int m_menuSceneIndex = 0;
    [Tooltip("The camera to show upon death")]
    public Camera m_deathCamera;

    private EPlayerState m_playerState = EPlayerState.Normal;
    private CameraManager m_cameraManager;
    private int m_cameraNum = 0;

    void Start()
    {
        m_cameraManager = FindObjectOfType<CameraManager>();
        m_cameraNum = m_cameraManager.AddCamera(m_deathCamera);
    }

	void Update () 
    {
        // Display the proper message based on current state
	    switch(m_playerState)
        {
            case EPlayerState.Win:
                DisplayMessage("WINNER!\n\n[Press Space to continue]");
                break;
            case EPlayerState.Death:
                DisplayMessage("YOU DEAD!\n\n[Press Space to restart]\n[Press ESC to exit]");
                m_cameraManager.SwitchToCamera(m_cameraNum);
                break;
            case EPlayerState.Normal:
            default:
                DisplayMessage("");
                break;
        }

        // Check for input while in certain states
        if(m_playerState == EPlayerState.Win)
        {
            if(Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(m_menuSceneIndex);   
            }
        }
        else if (m_playerState == EPlayerState.Death)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(m_menuSceneIndex); 
            }
        }

	}

    // Set a new player state
    public void SetPlayerState(EPlayerState newState)
    {
        m_playerState = newState;
    }

    // Display a message
    private void DisplayMessage(string message)
    {
        m_textBox.text = message;
    }
}
