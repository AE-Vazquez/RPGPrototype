using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

    //TODO: Refactor this not to be assigned in script
    [SerializeField] const int m_walkableLayer = 8;
    [SerializeField] const int m_enemyLayer = 9;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster m_cameraRaycaster;
    AICharacterControl m_AICharacterControl;
    NavMeshAgent m_navMeshAGent;

    Vector3 m_currentDestination;
    Vector3 m_clickPoint;
    Transform m_walkTarget  ;

    bool m_isInDirectMode = false; //Flag to switch between mouse & gamepad controller



    void Start()
    {
        m_cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_AICharacterControl = GetComponent<AICharacterControl>();
        m_navMeshAGent = GetComponent<NavMeshAgent>();

        m_currentDestination = transform.position;

        m_cameraRaycaster.OnMouseClickEvent += ProcessMouseClick;

        m_walkTarget = new GameObject("PlayerWalkTarget").transform;
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch(layerHit)
        {
            case m_walkableLayer:
                m_walkTarget.transform.position = raycastHit.point;
                m_AICharacterControl.SetTarget(m_walkTarget);
                break;
            case m_enemyLayer:
                m_AICharacterControl.SetTarget(raycastHit.collider.transform);
                break;
            default:
                m_AICharacterControl.SetTarget(null);
                break;

        }

    }

    void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate move direction to pass to character
        Vector3 m_Move;
        if (Camera.main != null)
        {
            Transform m_CamTransform = Camera.main.transform;
            // calculate camera relative direction to move:
            Vector3 m_CamForward = Vector3.Scale(m_CamTransform.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_CamTransform.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        m_Character.Move(m_Move, false, false);
    }
    
}

