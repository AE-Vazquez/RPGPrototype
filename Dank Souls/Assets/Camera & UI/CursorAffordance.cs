using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [Header("Walk")]
    [SerializeField]
    Texture2D walkCursor = null;
    [SerializeField]
    Vector2 walkCursorOffset = new Vector2(96, 96);

    [Header("Attack")]
    [SerializeField]
    Texture2D attackCursor = null;
    [SerializeField]
    Vector2 attackCursorOffset = new Vector2(96, 96);

    [Header("Unknown")]
    [SerializeField]
    Texture2D unknownCursor = null;
    [SerializeField]
    Vector2 unknownCursorOffset = Vector2.zero;


    CameraRaycaster m_CameraRayCaster;
	// Use this for initialization
	void Start () {
        m_CameraRayCaster = GetComponent<CameraRaycaster>();

    }
	
	// Update is called once per frame
	void Update () {

        if (m_CameraRayCaster != null)
        {
            switch (m_CameraRayCaster.layerHit)
            {
                case Layers.Walkable:
                    Cursor.SetCursor(walkCursor, walkCursorOffset, CursorMode.Auto);
                    break;
                case Layers.Enemy:
                    Cursor.SetCursor(attackCursor, attackCursorOffset, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(unknownCursor, unknownCursorOffset, CursorMode.Auto);
                    break;
            }
        }


    }
}
