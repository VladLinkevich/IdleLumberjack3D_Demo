using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel = null;

    public void Play()
    {
        startPanel.SetActive(false); ;
    }
}
