using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AxeManager : MonoBehaviour
{
    public static AxeManager instance = null;

    [SerializeField] private GameObject axeUI = null;
    [SerializeField] private Button levelUpButton = null;
    [SerializeField] private TMP_Text currentLevelText = null;
    [SerializeField] private TMP_Text damageText = null;
    [SerializeField] private TMP_Text costLevelText = null;
    [SerializeField] private int level = 1;
    [SerializeField] private float damage = 1;

    public float Damage => damage;

    private void Start()
    {
        if (instance == null) { instance = this; }

        RefrechText(); 
    }

    public void LevelUp()
    {
        if (CoinManager.instance.BuySomething(CostLevelUp()))
        {
            level++;
            damage *= 2;

            RefrechText();
            CheckYourCoin();
        } else { levelUpButton.enabled = false; }

    }

    public void OpenPanel()
    {
        axeUI.SetActive(true);
        CheckYourCoin();
    }

    public void ClosePanel()
    {
        axeUI.SetActive(false);
    }

    private void RefrechText()
    {
        currentLevelText.text = $"Lvl: {level}";
        damageText.text = $"Damage: {damage}";
        costLevelText.text = string.Format("Coin: {0:0.0}", CostLevelUp());
    }

    private decimal CostLevelUp()
    {
        return (decimal)Mathf.Pow(level, 1.5f);
    }

    private void CheckYourCoin()
    {
        if (CoinManager.instance.CheckYourCoin(CostLevelUp()))
        {
            levelUpButton.enabled = true;
        }
        else { levelUpButton.enabled = false; }
    }
}
