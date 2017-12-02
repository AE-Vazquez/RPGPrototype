using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour,IDamageable
{
    [SerializeField]
    float m_maxHealthPoints=100f;

    float m_currentHealthPoints;

    public float HealthAsPercentage
    {
        get { return m_currentHealthPoints / m_maxHealthPoints; }
    }

    void Awake()
    {
        m_currentHealthPoints = m_maxHealthPoints;

    }

	
    void IDamageable.TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " Taking " + damage + " damage");
        m_currentHealthPoints = Mathf.Clamp(m_currentHealthPoints - damage, 0f, m_maxHealthPoints);

        if(m_currentHealthPoints<=0)
        {
           
            //TODO: Handle death

        }

    }
}
