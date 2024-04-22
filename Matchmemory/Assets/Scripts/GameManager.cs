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

    public PopupManager popupManager;

    [SerializeField] private AudioManager audioManager;

    public List<TileScript> TileScriptList = new List<TileScript>();

    public static Action DestroyTiles;

    public int Score = 0;
    public int TotalTiles = 0;
    public int tilesMatched = 0;

    public bool isLevelCompleted = false;


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
                audioManager.PlayFeedbackAudio(0); // 0 for true
                tileX.OnTileMatch();
                tileY.OnTileMatch();
                Score += 2;
                uiManager.DisplayScore();
            }
            else
            {
                audioManager.PlayFeedbackAudio(1); // 1 for false
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
        tilesMatched = 0;
        TotalTiles = 0;
        uiManager.mainMenuBtn.gameObject.SetActive(false);

        for(int i = 0;i < TileScriptList.Count;i++)
        {
            Destroy(TileScriptList[i].gameObject);
        }

        TileScriptList.Clear();
        Debug.Log($"The tilescript list is : {TileScriptList.Count}");

        uiManager.MainMenuUI.SetActive(true);
        Debug.Log("inside the restart game end ******* ");
    }

    public void CheckLevelcompleted()
    {
        if(tilesMatched < TotalTiles)
        {
            tilesMatched ++;
            Debug.Log("The tiles matched are : " + tilesMatched);
        }
        
        if(tilesMatched == TotalTiles) 
        {
            isLevelCompleted = true;    
            audioManager.PlayFeedbackAudio(2);
        }
    }
}
