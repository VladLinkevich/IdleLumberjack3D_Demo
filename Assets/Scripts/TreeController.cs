using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private GameObject[] partsTree = null;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Hit(float damage)
    {
        Debug.Log($"Tree: {gameObject.name}. Health: {health}");
        health -= damage;

        if (health <= 0) { Destroy(gameObject); }

        for (float i = 0.0f, end = 1 - (health / maxHealth); i < end;  i += 0.2f)
        {
            partsTree[(int)(i * 5)].SetActive(false);
        }
    }

}
