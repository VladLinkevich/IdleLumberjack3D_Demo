using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager inctance = null;

    [SerializeField] private GameObject settingPanel = null;
    [SerializeField] private Image effectButton = null;

    private bool effect = true;

    public bool Effect => effect;

    private void Start()
    {
        if(inctance == null) { inctance = this; }
    }

    public void Close()
    {
        settingPanel.SetActive(false);
    }

    public void Open()
    {
        settingPanel.SetActive(true); ;
    }

    public void EffectButton()
    {
        effect = !effect;
        if (effect) { effectButton.color = Color.green; }
        else { effectButton.color = Color.red; }
    }
}
