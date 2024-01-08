using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHungerBar : MonoBehaviour
{

    [SerializeField] private Image _hungerbar;

    public void UpdateHungerBar(float maxHunger, float currentHunger)
    {
        _hungerbar.fillAmount = currentHunger / maxHunger;
    }

    // Update is called once per frame
}
