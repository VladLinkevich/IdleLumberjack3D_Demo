using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance = null;

    [SerializeField] private GameObject homeUI = null;
    [SerializeField] private Button levelUpButton = null;

    [SerializeField] private TMP_Text currentLevelText = null;
    [SerializeField] private TMP_Text storageText = null;
    [SerializeField] private TMP_Text coinVelocityText = null;
    [SerializeField] private TMP_Text costLevelText = null;

    [SerializeField] private int level = 1;

    private decimal _coin = 0;
    private float _coinInSec = 0f;


    private void Start()
    {
        if (instance == null) { instance = this; }

        StartCoroutine(CounterCoin());
        RefrechText();
    }

    IEnumerator CounterCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _coin += (decimal)_coinInSec;
            RefrechText();
        }
    }

    public void LevelUp()
    {
        if (CoinManager.instance.BuySomething(CostLevelUp()))
        {
            level++;
            _coinInSec += 0.1f;

            RefrechText();
            CheckYourCoin();
        }
        else { levelUpButton.enabled = false; }

    }

    public void Collect()
    {
        CoinManager.instance.AddCoin(_coin);
        _coin = 0;
    }

    public void OpenPanel()
    {
        homeUI.SetActive(true);
        CheckYourCoin();
    }

    public void ClosePanel()
    {
        homeUI.SetActive(false);
    }

    private void RefrechText()
    {
        currentLevelText.text = $"Lvl: {level}";
        storageText.text = string.Format("Storage: {0:0.0}", _coin);
        coinVelocityText.text = string.Format("Coin in sec: {0:0.0}", _coinInSec);
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
