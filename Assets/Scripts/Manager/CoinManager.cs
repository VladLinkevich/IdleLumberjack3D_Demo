using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance = null; 

    [SerializeField] private TMP_Text textCoin = null;

    private decimal _coins = 0;
    private List<Tree> trees;

    private void Start()
    {
        if (instance == null) { instance = this; }
        textCoin.text = $"Coin: 0";
    }

    public void SellTree()
    {
        trees = TreeCounterManager.instance.Trees;

        foreach(Tree tree in trees)
        {
            _coins += CoinLevel(tree.Level) * tree.Count;
        }

        SetTextUI();
        TreeCounterManager.instance.RemoveTree();
    }

    public void AddCoin(decimal coin)
    {
        _coins += coin;
        SetTextUI();
    }

    public bool BuySomething(decimal coin)
    {
        if (_coins >= coin) 
        { 
            _coins -= coin;
            SetTextUI();
            return true;
        } else { return false; }
    }

    public bool CheckYourCoin(decimal coin)
    {
        if (_coins >= coin) { return true; }
        else { return false; }
    }

    private void SetTextUI()
    {
        if (_coins / 1000000000 >= 1) { textCoin.text = string.Format("Coin: {0:0.0} B", _coins / 1000000000);  }
        else if (_coins / 1000000 >= 1) { textCoin.text = string.Format("Coin: {0:0.0} M", _coins / 1000000);  }
        else if (_coins / 1000 >= 1) { textCoin.text = string.Format("Coin: {0:0.0} K", _coins/1000);  }
        else { textCoin.text = string.Format("Coin: {0:0}", _coins); }
    }

    private decimal CoinLevel(int level)
    {
        return (decimal)Mathf.Pow(level, 1.5f) - (decimal)Mathf.Pow(level, 1.5f) % 1;
    }
}
