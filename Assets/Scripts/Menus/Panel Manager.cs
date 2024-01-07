using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PanelManager : MonoBehaviour
{
    public GameObject PauseMenu; 
    public GameObject SettingsPanel; 

    public void OpenSettingsPanel()
    {
        PauseMenu.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        SettingsPanel.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
