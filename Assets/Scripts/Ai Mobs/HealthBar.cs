using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthbar;

    private Camera _camera;
    // Start is called before the first frame update

    private void Start()
    {
        _camera = Camera.main;  
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbar.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
    // Update is called once per frame
}
