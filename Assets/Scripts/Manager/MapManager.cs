using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    static public MapManager instance = null;

    [SerializeField] private GameObject levelObj = null;
    [SerializeField] private float lenghtLevel = 0;

    [SerializeField] private GameObject treeObject = null;
    [SerializeField] private int countTrees = 100;
    [SerializeField] private GameObject[] homeObjects = null;


    private List<GameObject> forest = null;

    private const int FIRSTLEVELDISTANCE = -35;
    private const int SECONDLEVELDISTANCE = -15;
    private const int THIRDLEVELDISTANCE = 5;
    private const int FOURTHLEVELDISTANCE = 25;
    private const float OFFSETX = 4.5f;
    private const float OFFSETY = 20;

    private void Start()
    {
        if (instance == null) { instance = this; }

        forest = new List<GameObject>();
       
        CreateForest(forest, levelObj.transform.position);
    }

    public void UpdateScene()
    {
        levelObj.transform.position += new Vector3(0, 0, lenghtLevel);

        foreach (GameObject obj in homeObjects)
        {
            obj.transform.position += new Vector3(0, 0, lenghtLevel);
        }

        foreach (GameObject tree in forest)
        {
            tree.SetActive(true);
            tree.transform.position += new Vector3(0, 0, lenghtLevel);
            tree.GetComponent<TreeController>().LevelUp();
        }
    }

    private void CreateForest(List<GameObject> forest, Vector3 offset)
    {
        CreateZoneTrees(forest, offset, 1, FIRSTLEVELDISTANCE);
        CreateZoneTrees(forest, offset, 2, SECONDLEVELDISTANCE);
        CreateZoneTrees(forest, offset, 3, THIRDLEVELDISTANCE);
        CreateZoneTrees(forest, offset, 4, FOURTHLEVELDISTANCE);

    }

    private void ResetLevel(List<GameObject> forest, GameObject levelObj)
    {
        
    }

    private void CreateZoneTrees(List<GameObject> forest, Vector3 offset, int level, int startPos)
    {
        for (int i = 0; i < countTrees; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(-OFFSETX, OFFSETX), 1,
                Random.Range(0, OFFSETY) + startPos) + offset;

            GameObject buf = Instantiate(treeObject, pos, Quaternion.Euler(new Vector3(0, Random.Range(0, 90), 0)));
            buf.GetComponent<TreeController>().SetLevel(level);
            forest.Add(buf);
        }
    }
}
