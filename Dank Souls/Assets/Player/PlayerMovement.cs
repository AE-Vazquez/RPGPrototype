using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]
    float m_walkMoveRadius = 0.2f; //Minimum distance required to make the character move
    [SerializeField]
    float m_attackMoveRadius = 0.2f; //Minimum distance required to make the attack

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster m_cameraRaycaster;
    Vector3 m_currentDestination;

    Vector3 m_clickPoint;

    bool m_isInDirectMode = false; //Flag to switch between mouse & gamepad controller

    private void Start()
    {
        m_cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            m_isInDirectMode = !m_isInDirectMode;
            m_currentDestination = transform.position;

        }

        if (m_isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
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

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            m_clickPoint = m_cameraRaycaster.RayHit.point;
            switch (m_cameraRaycaster.LayerHit)
            {
                case Layers.Walkable:
                    
                    m_currentDestination = ShortDestination(m_clickPoint, m_walkMoveRadius);
                    break;
                case Layers.Enemy:

                    m_currentDestination = ShortDestination(m_clickPoint, m_attackMoveRadius);
                    break;
                default:
                    m_currentDestination = transform.position;
                    break;
            }

        }
        WalkToPosition();
    }

    private void WalkToPosition()
    {
        Vector3 moveVector = m_currentDestination - transform.position;

        if (moveVector.magnitude >= 0.05f)
        {
            m_Character.Move(moveVector, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortFactor)
    {
        Vector3 reduction = (destination - transform.position).normalized * shortFactor;
        return destination - reduction;

    }

    void OnDrawGizmos()
    {
        //Movement Gizmos
        Gizmos.color = new Color(0, 0, 0, 0.6f);
        Gizmos.DrawLine(transform.position, m_clickPoint);

        Gizmos.DrawSphere(m_clickPoint, 0.05f);

        Gizmos.DrawSphere(m_currentDestination, 0.1f);

        //Attack Gizmos
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawWireSphere(transform.position, m_attackMoveRadius);


    }
}

