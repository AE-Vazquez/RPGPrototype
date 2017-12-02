using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : CharacterBase {

    [SerializeField] float m_chaseRadius;
    [SerializeField] float m_attackRadius;
    [SerializeField] float m_rangedAttackRadius;

    private Vector3 m_initialPosition;

    AICharacterControl m_AICharacterControl;
    NavMeshAgent m_navMeshAgent;

    GameObject m_playerGO;
    private float aux_distanceToPlayer;

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

        //TODO refactor this
        m_AICharacterControl.SetTarget(null);
        m_navMeshAgent.SetDestination(transform.position);

        aux_distanceToPlayer = Vector3.Distance(m_playerGO.transform.position, transform.position);

        if (aux_distanceToPlayer <= m_rangedAttackRadius)
        {
            //TODO: ranged attack against player
            Debug.Log("Ranged Attack against player");
        }
        else if (aux_distanceToPlayer <= m_chaseRadius)
        {
            if(aux_distanceToPlayer<=m_attackRadius)
            {
                //TODO: melee attack against player
                Debug.Log("Melee Attack against player");
            }
            else
            {
                m_AICharacterControl.SetTarget(m_playerGO.transform);
            }
            
        }
        else
        {
            m_navMeshAgent.SetDestination(m_initialPosition);
        }
	}

    void OnDrawGizmos()
    {
        //Chasing range gizmo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_chaseRadius);

        //Melee Attack Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRadius);

        //Ranged Attack Gizmos
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, m_rangedAttackRadius);


    }


}
