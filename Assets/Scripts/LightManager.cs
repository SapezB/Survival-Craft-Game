using UnityEngine;
using TMPro;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float daySpeed;
    public int currTime;
    private bool dayIncremented;
    public int numDays;
    public TextMeshProUGUI dayText;

    private void Start() {
        TimeOfDay = 10;
        numDays = 0;
        dayIncremented = false;
    }

    private void Update()
    {
        calculateTime();
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += daySpeed;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f );
        }
        else
        {
            UpdateLighting(TimeOfDay  / 24f );
        }
        dayText.text = "Day : " + numDays.ToString();
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbColour.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColour.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirColour.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    private int calculateTime(){
        currTime = (int) TimeOfDay;
        if(currTime == 0 && !dayIncremented){
            numDays +=1;
            dayIncremented = true;
        }
        else if(currTime > 0){
            dayIncremented = false;
        }
        return currTime;
    }
}