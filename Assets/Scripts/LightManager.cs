using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset preset;

    [SerializeField, Range(0,24)] private float TimeOfDay;

    private void Update() {
        if(preset == null){
            return;
        }
        if(Application.isPlaying){
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay/24f);
        }
        else{
            UpdateLighting(TimeOfDay/24f);
        }
    }

    private void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = preset.AmbColour.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColour.Evaluate(timePercent);

        if(DirectionalLight != null){
            DirectionalLight.color = preset.DirecColour.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent* 360f)-90f,1704,0));
        }
    }
}
