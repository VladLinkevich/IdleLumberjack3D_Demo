using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class Tree
{
    public GameObject textObject;
    public TMP_Text text;
    public int level;
    public int count;

    public Tree(GameObject textObj, string nameParent, int level) {
        textObj.transform.SetParent(GameObject.Find(nameParent).transform);

        textObject = textObj;
        text = textObj.GetComponent<TMP_Text>();
        this.level = level;
        count = 1;

        text.text = $"Lvl: {level} - {count}";
    }

    public void Increment()
    {
        count++;
        text.text = $"Lvl: {level} - {count}";
    }
}

public class TreeCounterManager : MonoBehaviour
{
    public static TreeCounterManager instance = null;

    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject parentForText;

    private List<Tree> _trees = null;

    private void Start()
    {
        if(instance == null) { instance = this; }
        _trees = new List<Tree>();
    }

    public void AddTree(int level)
    {
        for(int i = 0, end = _trees.Count; i < end; ++i)
        {
            if (_trees[i].level == level) { 
                _trees[i].Increment();
                return;
            }
        }

        _trees.Add(new Tree(Instantiate(textObject), parentForText.name, level));

    }
}
