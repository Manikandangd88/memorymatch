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
}
