using UnityEngine;
using System.Collections;

public class TargetReticle : MonoBehaviour {

    public float speed;
    public bool m_canControl;

	void Update () {
        if (m_canControl)
        {
            float v = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
        }
	}

    public void SetControl(bool b)
    {
        m_canControl = b;
    }
}
