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
    }

    public void Hit(float damage)
    {
        Debug.Log($"Tree: {gameObject.name}. _health: {_health}");
        _health -= damage;

        if (_health <= 0)
        { 
            Destroy(gameObject);
            TreeCounterManager.instance.AddTree(_level);
        }


        for (float i = 0f, end = (1 - (_health / maxHealth)) * partsTree.Length - 1; i < end;  i += 1f)
        {
            partsTree[(int)i].SetActive(false);
        }
    }

    public void SetLevel(int level)
    {
        _level = level;
        maxHealth *= Mathf.Pow(_level, 1.5f);
    }

}
