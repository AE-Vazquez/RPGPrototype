using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase {


    [SerializeField] float meleeDamage = 10f;
    [SerializeField] float meleeAttackDelay = 0.5f;
    [SerializeField] float meleeRange = 2f;

    CameraRaycaster cameraRayCaster;

    GameObject currentTarget;

    float lastHitTime;

    //TODO: Refactor this not to be assigned in script
    [SerializeField] const int m_enemyLayer = 9;

    void Start()
    {
        cameraRayCaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRayCaster.OnMouseClickEvent += ProcessMouseClick;

    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit==m_enemyLayer)
        { 
                currentTarget = raycastHit.collider.gameObject;

            if (Vector3.Distance(currentTarget.transform.position, transform.position) > meleeRange)
            {
                return;
            }


            if (Time.time - lastHitTime > meleeAttackDelay)
            { 
                currentTarget.GetComponent<IDamageable>().TakeDamage(meleeDamage);
                lastHitTime = Time.time;
            }

        }

    }

    protected override void Die()
    {
        //TODO: Deal with player death
    }
}
