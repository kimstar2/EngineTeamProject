using System;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;

    private void Awake()
    {
        _settingPanel.SetActive(false);
    }
    
    public void SettingOnClick()
    {
        _settingPanel.SetActive(true);
    }

    public void SettingOffClick()
    {
        _settingPanel.SetActive(false);
    }
}
