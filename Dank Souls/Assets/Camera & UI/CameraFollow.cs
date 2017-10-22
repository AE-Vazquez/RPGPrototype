using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform _mTarget;


	void Start () {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");

        if (playerGO != null)
            _mTarget = playerGO.transform;
	}
	

	void LateUpdate () {
        if (_mTarget != null)
            transform.position = _mTarget.position;
	}
}
