using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button submit;

    [SerializeField] private GameObject MainMenuUI = null;
    [SerializeField] private GameObject FetchRowsAndColumnsUI = null;

    [SerializeField] private TMP_InputField rows = null;
    [SerializeField] private TMP_InputField columns = null;

    [SerializeField] private Button NewBtn;
    [SerializeField] private Button LoadBtn;
    [SerializeField] private Button QuitBtn;

    private void Awake()
    {
        InitializeGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGame()
    {
        submit.onClick.AddListener(OnSubmitPressed);

        MainMenuUI.SetActive(true);
        NewBtn.onClick.AddListener(() => { MainMenu("new"); });
        LoadBtn.onClick.AddListener(() => { MainMenu("load"); });
        QuitBtn.onClick.AddListener(() => { MainMenu("quit"); });
    }

    private void MainMenu(string btn)
    {
        switch(btn)
        {
            case "new":
                MainMenuUI.SetActive(false);
                GameManager.instance.dataStore.ResetGameData();
                FetchRowsAndColumnsUI.SetActive(true);
                break;
            case "load":
                MainMenuUI.SetActive(false);
                setGridLayoutProperties();
                GameManager.instance.dataStore.LoadData();
                break;
            case "quit":
                MainMenuUI.SetActive(false);
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void setGridLayoutProperties()
    {
        if (GameManager.instance.gridGenerator.GridLayoutComponent.constraint ==
                GridLayoutGroup.Constraint.FixedRowCount)
        {
            GameManager.instance.gridGenerator.GridLayoutComponent.constraintCount =
            GameManager.instance.gridGenerator.Rows;
        }
        else if (GameManager.instance.gridGenerator.GridLayoutComponent.constraint ==
                GridLayoutGroup.Constraint.FixedColumnCount)
        {
            GameManager.instance.gridGenerator.GridLayoutComponent.constraintCount =
                GameManager.instance.gridGenerator.Columns;
        }
    }

    /// <summary>
    /// Setting the rows and columns by getting input from user
    /// </summary>
    /// <param name="value"></param>
    public void SetRowsAndColumns(int value) // 0 for row ------ 1 for column
    {
        if (value == 0) 
        {
            GameManager.instance.gridGenerator.Rows = int.Parse(rows.text);

            if(GameManager.instance.gridGenerator.GridLayoutComponent.constraint ==
                GridLayoutGroup.Constraint.FixedRowCount)
            {
                GameManager.instance.gridGenerator.GridLayoutComponent.constraintCount =
                GameManager.instance.gridGenerator.Rows;
            }
            
        }
        else if (value == 1)
        { 
            GameManager.instance.gridGenerator.Columns = int.Parse(columns.text);

            if (GameManager.instance.gridGenerator.GridLayoutComponent.constraint ==
                GridLayoutGroup.Constraint.FixedColumnCount)
            {
                GameManager.instance.gridGenerator.GridLayoutComponent.constraintCount =
                GameManager.instance.gridGenerator.Columns;
            }
        }
    }

    private void OnSubmitPressed()
    {
        FetchRowsAndColumnsUI.SetActive(false);
        GameManager.instance.gridGenerator.GenerateTheGrid();
        //StartCoroutine(GameManager.instance.gridGenerator.GenerateTheGrid());
    }
}
