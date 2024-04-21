using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public Transform TileSpawnParent { get => tileSpawnParent; set => tileSpawnParent = value; }

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
    //public IEnumerator GenerateTheGrid()
    {
        GameObject go = null;
        TileScript tilescript = null;

        for (int i = 0; i < currentRows; i++)
        {
            for (int j = 0; j < currentColumns; j++)
            {
                go = Instantiate(tilePrefab, TileSpawnParent, false);
                tilescript = go.GetComponent<TileScript>();                

                GameManager.instance.TileScriptList.Add(go.GetComponent<TileScript>());
            }
        }
        //yield return new WaitForSeconds(1f);
        SetTheTileAttributes();
    }

    public void SetTheTileAttributes()
    {
        GameData gd = GameManager.instance.dataStore.gameData;

        int tempmaxtile = currentRows * currentColumns;
        int temptileid = 0;

        if (GameManager.instance.dataStore.IsGameSaved)
        {
            for(int i = 0; i < TileSpawnParent.childCount; i++)
            {
                GameManager.instance.TileScriptList[i].ID = gd.tiles[i].ID;
                GameManager.instance.TileScriptList[i].name = gd.tiles[i].ID.ToString();
                GameManager.instance.TileScriptList[i].transform.GetChild(0)
                    .GetComponent<TextMeshProUGUI>().text = gd.tiles[i].ID.ToString();

                if (!gd.tiles[i].TileActiveStatus)
                    GameManager.instance.TileScriptList[i].OnTileMatch();

            }            
        }
        else
        {
            for (int i = 0; i < tempmaxtile; i += 2)
            {
                for (int j = i; j < i + 2; j++)
                {
                    GameManager.instance.TileScriptList[j].ID = temptileid;
                    GameManager.instance.TileScriptList[j].name = temptileid.ToString();
                    GameManager.instance.TileScriptList[j].gameObject.transform.GetChild(0)
                    .GetComponent<TextMeshProUGUI>().text = temptileid.ToString();

                    GameManager.instance.TileScriptList[j].transform.SetSiblingIndex(Random.Range(0, tempmaxtile));
                }
                temptileid++;
            }
        }
    }    
}
