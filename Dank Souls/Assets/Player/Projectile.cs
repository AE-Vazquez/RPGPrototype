using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float m_damageCaused = 5f;
    [SerializeField] float m_projectileSpeed = 15f;


    public void SetDamage(float damage)
    {
        m_damageCaused = damage;
    }

    public void Launch(Vector3 target)
    {
        GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * m_projectileSpeed;
    }

	void OnTriggerEnter(Collider other)
    {
        //Dont collide with parent object
        if (transform.parent.parent == other.transform)
            return;

        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(m_damageCaused);
            Destroy(gameObject);
        }


    }
}
