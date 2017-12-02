using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float m_damageCaused = 5f;

	void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
            other.GetComponent<IDamageable>().TakeDamage(m_damageCaused);


    }
}
