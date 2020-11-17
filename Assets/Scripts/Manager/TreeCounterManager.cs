using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tree
{
    private GameObject textObject;
    private TMP_Text text;
    private int level;
    private int count;

    public int Level => level;
    public int Count => count;
    public GameObject TextObject => textObject;

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

    [SerializeField] private GameObject textObject = null;
    [SerializeField] private GameObject parentForText = null;

    private List<Tree> _trees = null;

    public List<Tree> Trees => _trees;

    private void Start()
    {
        if(instance == null) { instance = this; }
        _trees = new List<Tree>();
    }

    public void AddTree(int level)
    {
        for(int i = 0, end = _trees.Count; i < end; ++i)
        {
            if (_trees[i].Level == level) { 
                _trees[i].Increment();
                return;
            }
        }

        _trees.Add(new Tree(Instantiate(textObject), parentForText.name, level));
    }

    public void RemoveTree()
    {
        foreach(Tree tree in _trees)
        {
            Destroy(tree.TextObject);
        }
        _trees.Clear();
    }
}
