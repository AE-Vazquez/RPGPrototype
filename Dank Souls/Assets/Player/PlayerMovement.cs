using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]
    float m_minMoveDistance = 0.2f; //Minimum distance required to make the character move
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster m_cameraRaycaster;
    Vector3 m_currentClickTarget;

    bool m_isInDirectMode = false;

    private void Start()
    {
        m_cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            m_isInDirectMode = !m_isInDirectMode;
            m_currentClickTarget = transform.position;

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

            switch (m_cameraRaycaster.LayerHit)
            {
                case Layers.Walkable:
                    m_currentClickTarget = m_cameraRaycaster.RayHit.point;
                    break;
                case Layers.Enemy:
                    Debug.Log("ATTACK ENEMY!");
                    break;
                default:
                    m_currentClickTarget = transform.position;
                    break;
            }

        }
        Vector3 moveVector = m_currentClickTarget - transform.position;

        if (moveVector.magnitude >= m_minMoveDistance)
        {
            m_Character.Move(moveVector, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero,false,false);
        }
    }
}

