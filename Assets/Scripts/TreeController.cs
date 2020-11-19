using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private GameObject[] partsTree = null;
    
    [SerializeField] private int _level = 1;
    private float _health;

    private void Start()
    {
        SetLevel(_level);
        _health = maxHealth;
        foreach (GameObject part in partsTree)
        {
            part.GetComponent<MeshRenderer>().material.color = ChoiseColor();
        }
    }

    public void Hit(float damage)
    {
        Debug.Log($"Tree: {gameObject.name}. _health: {_health}");
        _health -= damage;

        if (_health <= 0)
        {
            TreeCounterManager.instance.AddTree(_level);
            foreach(GameObject part in partsTree)
            {
                part.SetActive(true);
            }
            gameObject.SetActive(false);
            return;
        }


        for (int i = 0, end = (int)((1 - (_health / maxHealth)) * partsTree.Length); i < end;  i ++)
        {
            partsTree[i].SetActive(false);
        }
    }

    public void SetLevel(int level)
    {
        _level = level;
        maxHealth *= Mathf.Pow(_level, 1.5f);
        _health = maxHealth;
        ChoiseColor();
    }

    public void LevelUp()
    {
        SetLevel(_level + 4);
    }

    private Color ChoiseColor()
    {
        if (_level % 4 == 0) { return Color.green; }
        else if (_level % 4 == 1) { return Color.red; }
        else if (_level % 4 == 2) { return Color.blue; }
        else { return Color.yellow; }
    }
}
