using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class DataStore : MonoBehaviour
{
    public GameData gameData;
    [SerializeField] private Button Save;
    private string path = string.Empty;
    [SerializeField] private bool isGameSaved = false;
    public bool IsGameDataSaved = false;

    public bool IsGameSaved { get => isGameSaved; set => isGameSaved = value; }

    private void OnEnable()
    {
        path = Application.persistentDataPath + "/playerdata.json";

        //if (File.Exists(path))
        CheckIfGameDataAvailable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckIfGameDataAvailable()
    {
        if (File.Exists(path))
        {
            IsGameDataSaved = true;
            Debug.Log($"is game data saved : {IsGameDataSaved}");
        }
        else
        {
            Debug.Log($"is game data saved in else : {IsGameDataSaved}");
            IsGameDataSaved = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        if(GameManager.instance.isLevelCompleted) { return; }

        Transform tileparent = GameManager.instance.gridGenerator.TileSpawnParent;
        int ilimit = tileparent.childCount;

        gameData = new GameData();
        gameData.Rows = GameManager.instance.gridGenerator.Columns;
        gameData.Columns = GameManager.instance.gridGenerator.Rows;
        gameData.Score = GameManager.instance.Score;

        for (int i = 0; i < ilimit; i++)
        {
            TileData tiledat = new TileData();

            tiledat.ID = tileparent.GetChild(i).GetComponent<TileScript>().ID;
            tiledat.TileActiveStatus = tileparent.GetChild(i).GetComponent<TileScript>().ActiveStatus;

            gameData.tiles.Add(tiledat);
        }

        PushToJson(gameData);
    }

    /// <summary>
    /// Method to store data
    /// ID,score,rows, columns,tileactivestatus,
    /// </summary>
    public void PushToJson<T>(T data)
    {
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data Exists!. Deleting the old file and creating the new one");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing the file for the first time");
            }

            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }
        catch (Exception e)
        {
            Debug.Log($"Unable to dave data : {e.Message} {e.StackTrace}");
        }

        isGameSaved = true;
    }
     
    /// <summary>
    /// Method to Load data
    /// ID,score,rows, columns,tileactivestatus,
    /// </summary>
    public void LoadData()
     {
        Debug.Log("is game data available : " + GameManager.instance.dataStore.IsGameDataSaved);
        gameData = new GameData();
        string jsonstring = File.ReadAllText(path);
        IsGameDataSaved = true;

        try
        {
            if (File.Exists(path))
            {
                gameData = JsonConvert.DeserializeObject<GameData>(jsonstring);

                GameManager.instance.gridGenerator.Rows = gameData.Columns;
                GameManager.instance.gridGenerator.Columns = gameData.Rows;
                GameManager.instance.Score = gameData.Score;

                GameManager.instance.uiManager.setGridLayoutProperties();
                GameManager.instance.gridGenerator.GenerateTheGrid();
                GameManager.instance.uiManager.DisplayScore();
            }
            else
            {
                Debug.Log("No file exists!");
            }
        }
        catch(Exception e)
        {
            Debug.Log($"error : {e.Message} {e.StackTrace}");
        }
    }

    public void ResetGameData()
    {
        if (File.Exists(path))
        { 
            File.Delete(path); 
            IsGameDataSaved = false; 
        }
        else
        {
            IsGameDataSaved = false;
        }

        gameData = null;
    }

    private void OnApplicationQuit()
    {
        //SaveData();
    }

    private void OnDisable()
    {
        //SaveData();
    }
}

[Serializable]
public class GameData
{
    public int Score = 0;
    public int Rows = 0;
    public int Columns = 0;

    public List<TileData> tiles = new List<TileData>();
};

[Serializable]
public class TileData
{
    public int ID;
    public bool TileActiveStatus;
};

