using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
        
    private TileScript primaryTile;

    public UIManager uiManager;

    public GridGenerator gridGenerator;

    public DataStore dataStore;

    public List<TileScript> TileScriptList = new List<TileScript>();

    public static Action DestroyTiles;

    public int Score = 0;    

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;            
        }
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddToPair(TileScript tile)
    {
        if(primaryTile == null)
        {
            primaryTile = tile;
        }
        else
        {
            TileScript tileX = primaryTile;
            primaryTile = null;
            CheckMatch(tileX, tile);
        }
    }

    public void CancelPair()
    {
        primaryTile = null;
    }

    private void CheckMatch(TileScript tileX, TileScript tileY)
    {
        if (tileX != null && tileY != null)
        {
            //while (FlippedTiles[0] && )
            if (tileX.ID == tileY.ID)
            {
                tileX.OnTileMatch();
                tileY.OnTileMatch();
            }
            else
            {
                tileX.ResetTile();
                tileY.ResetTile();
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("inside the restart game start ******* ");
        primaryTile = null;
        Score = 0;
        
        gridGenerator.Rows = 0;
        gridGenerator.Columns = 0;
        uiManager.mainMenuBtn.gameObject.SetActive(false);
        //GameManager.instance.dataStore.ResetGameData();
        //DestroyTiles?.Invoke();

        for(int i = 0;i < TileScriptList.Count;i++)
        {
            Destroy(TileScriptList[i].gameObject);
        }

        TileScriptList.Clear();
        Debug.Log($"The tilescript list is : {TileScriptList.Count}");

        uiManager.MainMenuUI.SetActive(true);
        Debug.Log("inside the restart game end ******* ");
    }
}
