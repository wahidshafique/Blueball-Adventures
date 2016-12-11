using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {
	public bool manualLoad = false;
	
	void Update () {
		if (manualLoad)
			SceneManager.LoadScene(0);
	}

	public void World1 () {
        SceneManager.LoadScene("World1");
	}
    public void World2() {
        SceneManager.LoadScene("World2");
    }
    public void World3() {
        SceneManager.LoadScene("World3");
    }

    public void Quit () {
		Application.Quit();
	}
}
