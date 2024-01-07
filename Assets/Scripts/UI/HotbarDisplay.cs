using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HotbarDisplay : StaticInventoryDisplay
{
    private int maxIndex = 9;
    private int currentIndex = 0;

    private PlayerController playerController;
    void Start()
    {
        base.Start();

        currentIndex = 0;
        maxIndex = slots.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
