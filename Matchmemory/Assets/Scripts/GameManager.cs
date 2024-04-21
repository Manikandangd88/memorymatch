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
    public List<TileScript> FlippedTiles = new List<TileScript>();

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

    
}
