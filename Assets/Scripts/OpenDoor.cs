using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private LightManager light;

    // Update is called once per frame
    void Update()
    {
        if(light.numDays >= 5){
            Destroy(this.gameObject);
        }
    }
}
