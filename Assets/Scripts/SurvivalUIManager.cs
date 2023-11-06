using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUIManager : MonoBehaviour
{
    [SerializeField] private SurvivalManager survivalManager;
    [SerializeField] private Image hungerMeter, healthMeter;

    private void FixedUpdate()
    {
        hungerMeter.fillAmount = survivalManager.HungerPercent;
        healthMeter.fillAmount = survivalManager.HealthPercent;
    }
}
