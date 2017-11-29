using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] Image m_healthBarImage;
    [SerializeField] CharacterBase m_characterRef;

    // Use this for initialization
    void Start()
    {
        if (m_healthBarImage == null)
            m_healthBarImage = transform.Find("HealthBar_Foreground").GetComponent<Image>();

        if (m_characterRef == null)
            m_characterRef=transform.parent.parent.GetComponent<CharacterBase>();
    }

    // Update is called once per frame
    void Update()
    {
        m_healthBarImage.fillAmount= m_characterRef.HealthAsPercentage;
    }
}
