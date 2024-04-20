using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region PrivateFields

    public UIManager uiManager;
    public GridGenerator gridGenerator;

    public List<TileScript> TileScriptList = new List<TileScript>();
    public List<TileScript> FlippedTiles = new List<TileScript>();
    
    #endregion


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
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGame()
    {
        uiManager.FetchRowsAndColumnsUI.SetActive(true);
    }

    public void CheckMatch()
    {
        if(FlippedTiles.Count > 1)
        {
            //while (FlippedTiles[0] && )
            if (FlippedTiles[0].ID == FlippedTiles[1].ID)
            {
                FlippedTiles[0].GetComponent<Image>().enabled = false;
                FlippedTiles[0].GetComponent<Button>().enabled = false;
                FlippedTiles[0].GetComponent<TileScript>().enabled = false;

                for(int i = 0; i < FlippedTiles[0].transform.childCount; i++)
                {
                    FlippedTiles[0].transform.GetChild(i).gameObject.SetActive(false);
                }

                FlippedTiles[1].GetComponent<Image>().enabled = false;
                FlippedTiles[1].GetComponent<Button>().enabled = false;
                FlippedTiles[1].GetComponent<TileScript>().enabled = false;

                for (int i = 0; i < FlippedTiles[1].transform.childCount; i++)
                {
                    FlippedTiles[1].transform.GetChild(i).gameObject.SetActive(false);
                }

                for(int i = 0;i < 2; i++)
                {
                    FlippedTiles.Remove(FlippedTiles[0]);
                }
                //FlippedTiles.Remove(FlippedTiles[0]);
                //FlippedTiles.Remove(FlippedTiles[1]);
            }
            else 
            {
                FlippedTiles[0].OnTileTouched();
                FlippedTiles[1].OnTileTouched();

                for (int i = 0; i < 2; i++)
                {
                    FlippedTiles.Remove(FlippedTiles[0]);
                }
                //FlippedTiles.Remove(FlippedTiles[0]);
                //FlippedTiles.Remove(FlippedTiles[1]);
            }
        }
    }

    private TileScript primaryTile;

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

    private static void CheckMatch(TileScript tileX, TileScript tileY)
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
}
