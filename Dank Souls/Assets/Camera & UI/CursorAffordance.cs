using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [Header("Walk")]
    [SerializeField]
    Texture2D m_walkCursor = null;
    [SerializeField]
    Vector2 walkCursorOffset = new Vector2(96, 96);

    [Header("Attack")]
    [SerializeField]
    Texture2D m_attackCursor = null;
    [SerializeField]
    Vector2 attackCursorOffset = new Vector2(96, 96);

    [Header("Unknown")]
    [SerializeField]
    Texture2D m_unknownCursor = null;
    [SerializeField]
    Vector2 unknownCursorOffset = Vector2.zero;

    //TODO: Refactor this not to be assigned in script
    [SerializeField] const int m_walkableLayer = 8;
    [SerializeField] const int m_enemyLayer = 9;



    CameraRaycaster m_CameraRayCaster;
	// Use this for initialization
	void Start () {
        m_CameraRayCaster = GetComponent<CameraRaycaster>();
        m_CameraRayCaster.OnCursorLayerChangedEvent += OnLayerChanged;

    }
	
	void OnLayerChanged (int newLayer) {

        switch (newLayer)
        {
            case m_walkableLayer:
                Cursor.SetCursor(m_walkCursor, walkCursorOffset, CursorMode.Auto);
                break;
            case m_enemyLayer:
                Cursor.SetCursor(m_attackCursor, attackCursorOffset, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(m_unknownCursor, unknownCursorOffset, CursorMode.Auto);
                break;
        }
 

    }
}
