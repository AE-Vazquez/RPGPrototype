using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField]Image healthBarImage;
    [SerializeField]
    CharacterBase characterRef;

    // Use this for initialization
    void Start()
    {
        if (healthBarImage == null)
            healthBarImage = transform.Find("HealthBar_Foreground").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount= characterRef.HealthAsPercentage;
    }
}
