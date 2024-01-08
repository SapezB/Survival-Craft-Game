using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthbar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbar.fillAmount = currentHealth / maxHealth;
    }

    // Update is called once per frame
}
