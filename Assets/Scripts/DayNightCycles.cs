using TMPro;
using UnityEngine;

public class DayNightCycles : MonoBehaviour
{
    public float dayDuration = 60.0f; // Duration of a day in seconds
    public TextMeshProUGUI dayText;

   
    private bool isNight = false;
    public float timeOfDay = 0.0f; // 0.0 to 1.0, where 0 is sunrise and 0.5 is sunset
    public int dayCount = 1;


    private void Update()
    {
        // Calculate the time of day based on the game time
        timeOfDay += Time.deltaTime / dayDuration;

        // Check if it's night
        if (timeOfDay >= 0.5f)
        {
            if (!isNight)
            {
   
                isNight = true;
            }
        }
        else
        {
            if (isNight)
            {
      
                isNight = false;
                dayCount++;
            }
        }

        // Reset timeOfDay to keep it within the [0, 1] range
        timeOfDay %= 1.0f;

        // Update the sun's rotation based on time of day (0 to 180 degrees)
        transform.eulerAngles = new Vector3(timeOfDay * 180.0f, -30.0f, 0.0f);

        dayText.text = "Day : " + dayCount.ToString();
    }
}