using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void ShowPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
    }
}
