using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;

    private float m_xDegreesPerFrame;
    private float m_yDegreesPerFrame;
    private float m_zDegreesPerFrame;

    void Update () {
		m_xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute;
        transform.RotateAround (transform.position, transform.right, m_xDegreesPerFrame);

		m_yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
        transform.RotateAround (transform.position, transform.up, m_yDegreesPerFrame);

		m_zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
        transform.RotateAround (transform.position, transform.forward, m_zDegreesPerFrame);
	}
}
