using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemySight : MonoBehaviour {

    [SerializeField] float m_sightRadius = 5;
    [SerializeField] float m_fieldOfViewAngle=110f;

    private Vector3 m_lastSighting;
    bool m_playerInSight;

    GameObject m_playerGO;
    Vector3 m_directionToPlayer;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = m_sightRadius;
        m_lastSighting=Vector3.zero;
    }

    private void Start()
    {
        m_playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject== m_playerGO)
        {
            m_playerInSight = false;

            m_directionToPlayer = m_playerGO.transform.position - transform.position;
            float angle = Vector3.Angle(m_directionToPlayer, transform.forward);

            if (angle<m_fieldOfViewAngle*0.5f)
            {
                //TODO extract a function to do multiple raycasts to avoid one small obstacle to block sight
                RaycastHit hit;

                if(Physics.Raycast(transform.position+Vector3.up,m_directionToPlayer.normalized,out hit, m_sightRadius))
                {
                    if(hit.collider.gameObject ==m_playerGO)
                    {
                        m_playerInSight = true;
                        //TODO do some action on player in sight
                        Debug.Log("Player In Sight");
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_playerGO)
        {
            m_playerInSight = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 0, 255f, 1f);

        Quaternion leftRayRotation = Quaternion.AngleAxis(-m_fieldOfViewAngle * 0.5f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(m_fieldOfViewAngle * 0.5f, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * m_sightRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * m_sightRadius);

        Gizmos.DrawWireSphere(transform.position, m_sightRadius);
         
  
    }


}
