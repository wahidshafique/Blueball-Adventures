using UnityEngine;
using System.Collections;

public class TargetReticle : MonoBehaviour {

    [Tooltip("The speed of the reticle")]
    public float speed;
    [Tooltip("Can the reticle be controlled")]
    public bool m_canControl;

	void Update () {
        if (m_canControl)
        {
            // Move the reticle based on the vertical axis and speed
            float v = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
        }
	}

    // Can the reticel be controlled, true for yes, false for no
    public void SetControl(bool b)
    {
        m_canControl = b;
    }
}
