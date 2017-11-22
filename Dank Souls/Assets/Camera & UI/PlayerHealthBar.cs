using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField]Image healthBarImage;
    Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (healthBarImage == null)
            healthBarImage = transform.Find("HealthBar_Foreground").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount=player.HealthAsPercentage;
    }
}
