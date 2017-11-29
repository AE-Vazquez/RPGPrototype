using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : CharacterBase {

    [SerializeField] float m_chaseDistance;
    [SerializeField] float m_chaseStopRadius;

    private Vector3 m_initialPosition;

    AICharacterControl m_AICharacterControl;
    NavMeshAgent m_navMeshAgent;

    GameObject m_playerGO;

    void Awake()
    {
        m_AICharacterControl = GetComponent<AICharacterControl>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();

    }
    
	// Use this for initialization
	void Start () {

        //TODO: Change FindObject to differenty way
        m_playerGO = GameObject.FindGameObjectWithTag("Player");

        m_initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Vector3.Distance(m_playerGO.transform.position,transform.position)<=m_chaseDistance)
        {
            m_AICharacterControl.SetTarget(m_playerGO.transform);
        }
        else
        {
            m_AICharacterControl.SetTarget(null);
            m_navMeshAgent.SetDestination(m_initialPosition);
        }
	}
    
}
