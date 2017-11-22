using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {


    [SerializeField]
    float m_maxHealthPoints=100f;

    float m_currentHealthPoints;

    public float HealthAsPercentage
    {
        get { return m_maxHealthPoints / m_currentHealthPoints; }
    }

    void Awake()
    {
        m_currentHealthPoints = m_maxHealthPoints;

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        m_currentHealthPoints -= damage;

        if (m_currentHealthPoints <= 0)
        {
            m_currentHealthPoints = 0;
            //TODO: Generate death events
        }

        //TODO: Generate HP changes events

    }
}
