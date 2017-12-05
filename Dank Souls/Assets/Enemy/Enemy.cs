using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : CharacterBase {

    [SerializeField] float m_chaseRadius;
    [SerializeField] float m_attackRadius;

    [SerializeField] float m_rangedAttackRadius;
    [SerializeField] float m_rangedDamage = 8f;
    [SerializeField] float m_rangedCooldown = 1f;
    [SerializeField] GameObject m_rangedProjectile;
    [SerializeField] Transform m_projectileSpawn;
    [SerializeField] Vector3 m_aimOffset = new Vector3(0, 1, 0);

    private  IEnumerator shootingCoroutine;
    bool m_isShooting=false;

    private Vector3 m_initialPosition;

    AICharacterControl m_AICharacterControl;
    NavMeshAgent m_navMeshAgent;

    GameObject m_playerGO;
    private float aux_distanceToPlayer;

    void Awake()
    {
        base.Awake();
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

        if (m_isShooting)
        {
            if (aux_distanceToPlayer > m_rangedAttackRadius)
            {
                StopCoroutine(shootingCoroutine);
                m_isShooting = false;
            }
        }
        else
        {

            if (aux_distanceToPlayer <= m_rangedAttackRadius)
            {
                m_isShooting = true;
                shootingCoroutine = ShootCoroutine(m_playerGO, m_rangedCooldown);
                StartCoroutine(shootingCoroutine);

            }
            else if (aux_distanceToPlayer <= m_chaseRadius)
            {
                if (aux_distanceToPlayer <= m_attackRadius)
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

	}

    IEnumerator ShootCoroutine(GameObject target,float interval)
    {
        while (m_isShooting)
        {
            yield return new WaitForSeconds(interval);
            ShootAtTarget(target);
        }

    }

    void ShootAtTarget(GameObject target)
    {
        GameObject projectile = Instantiate(m_rangedProjectile, m_projectileSpawn);
        projectile.GetComponent<Projectile>().SetDamage(m_rangedDamage);
        projectile.GetComponent<Projectile>().Launch(target.transform.position+m_aimOffset);
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
