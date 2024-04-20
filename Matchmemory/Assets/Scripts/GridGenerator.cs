using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    #region Integers

    [SerializeField] private int currentRows, currentColumns = 0;

    [SerializeField] private const int androidMaxRows = 6;
    [SerializeField] private const int androidMaxColumns = 5;

    [SerializeField] private const int windowsMaxRows = 6;
    [SerializeField] private const int windowsMaxColumns = 6;

    #endregion

    [SerializeField] private GameObject tilePrefab = null;
    [SerializeField] private Transform tileSpawnParent = null;

    [SerializeField] private GridLayoutGroup gridLayoutComponent = null;

    public int Rows { get => currentRows; set => currentRows = value; }
    public int Columns { get => currentColumns; set => currentColumns = value; }
    public GridLayoutGroup GridLayoutComponent { get => gridLayoutComponent; set => gridLayoutComponent = value; }

    public int AndroidMaxRows => androidMaxRows;

    public int AndroidMaxColumns => androidMaxColumns;

    public int WindowsMaxRows => windowsMaxRows;

    public int WindowsMaxColumns => windowsMaxColumns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTheGrid()
    {
        GameObject go = null;
        

        for (int i = 0; i < currentRows; i++)
        {
            for(int j = 0; j < currentColumns; j++)
            {         
                go = Instantiate(tilePrefab, tileSpawnParent, false);
                GameManager.instance.TileScriptList.Add(go.GetComponent<TileScript>());
                
            }
        }

        SetTheTileAttributes();
    }

    public void SetTheTileAttributes()
    {
        int tempmaxtile = currentRows * currentColumns;
        int temptileid = 0;

        for (int i = 0; i < tempmaxtile; i+=2)
        {
            for(int j = i;j < i+2; j++)
            {    
                GameManager.instance.TileScriptList[j].ID = temptileid;
                GameManager.instance.TileScriptList[j].name = temptileid.ToString();

                GameManager.instance.TileScriptList[j].transform.SetSiblingIndex(Random.Range(0, tempmaxtile));
            }
            temptileid++;            
        }
    }    
}
