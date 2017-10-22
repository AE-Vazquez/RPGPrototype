using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_minMoveDistance = 0.2f; //Minimum distance required to make the character move
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {

            switch(cameraRaycaster.layerHit)
            {
                case Layers.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;
                case Layers.Enemy:
                    Debug.Log("ATTACK ENEMY!");
                    break;
                default:
                    break;
            }

        }
        Vector3 moveVector = currentClickTarget - transform.position;

        if (moveVector.magnitude >= m_minMoveDistance)
        {
            m_Character.Move(moveVector, false, false);
        }
    }
}

