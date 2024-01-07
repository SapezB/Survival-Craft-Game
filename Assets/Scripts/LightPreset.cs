using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LightPreset", menuName = "Survival-Craft-Game/LightPreset", order = 1)]
public class LightPreset : ScriptableObject {
    public Gradient AmbColour;
    public Gradient DirecColour;
    public Gradient FogColour;
}

