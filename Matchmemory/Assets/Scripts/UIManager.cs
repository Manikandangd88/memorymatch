using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button submit;

    [SerializeField] private GameObject mainMenuUI = null;
    [SerializeField] private GameObject FetchRowsAndColumnsUI = null;

    [SerializeField] private TMP_InputField rows = null;
    [SerializeField] private TMP_InputField columns = null;

    [SerializeField] private Button newBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button saveBtn;

    [SerializeField] private TextMeshProUGUI scoreText = null;

    public Button mainMenuBtn;

    public GameObject MainMenuUI { get => mainMenuUI; set => mainMenuUI = value; }

    private void Awake()
    {
        
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
        submit.onClick.AddListener(OnSubmitPressed);

        mainMenuUI.SetActive(true);
        newBtn.onClick.AddListener(() => { MainMenu("new"); });
        loadBtn.onClick.AddListener(() => { MainMenu("load"); });
        quitBtn.onClick.AddListener(() => { MainMenu("quit"); });
        saveBtn.onClick.AddListener(() => { MainMenu("save"); });


        //quitBtn.onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
        mainMenuBtn.onClick.AddListener(() => StartCoroutine(GoToMainMenu()));

        if(GameManager.instance.dataStore.IsGameDataSaved) { loadBtn.interactable = true; }
        else { loadBtn.interactable = false;}
        GameManager.instance.popupManager.ShowFeedback("initcomment");
    }

    private void MainMenu(string btn)
    {
        switch(btn)
        {
            case "new":
                mainMenuUI.SetActive(false);
                GameManager.instance.dataStore.ResetGameData();
                FetchRowsAndColumnsUI.SetActive(true);
                break;
            case "load":
                Debug.Log($"is game data available : {GameManager.instance.dataStore.IsGameDataSaved}");
                if (!GameManager.instance.dataStore.IsGameDataSaved)
                    return;
                
                mainMenuUI.SetActive(false);
                GameManager.instance.dataStore.LoadData();
                mainMenuBtn.gameObject.SetActive(true);
                saveBtn.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                break;
            case "save":
                GameManager.instance.dataStore.SaveData();
                break;
            case "quit":
#if UNITY_EDITOR                
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif               
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
        if (!rows.IsActive() || !columns.IsActive())
            return;

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
        if(rows.text == string.Empty || columns.text == string.Empty)
        {
            GameManager.instance.popupManager.ShowFeedback("submiterror");
            return;
        }

        if(GameManager.instance.gridGenerator.Rows <= 6 && GameManager.instance.gridGenerator.Columns <= 5)
        {

        }
        else
        {
            GameManager.instance.popupManager.ShowFeedback("initcomment");
            return;
        }

        FetchRowsAndColumnsUI.SetActive(false);
        GameManager.instance.gridGenerator.GenerateTheGrid();
        //mainMenuBtn.gameObject.SetActive(true);

        rows.text = string.Empty;
        columns.text = string.Empty;
        saveBtn.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
        
    private IEnumerator GoToMainMenu()
    {
        saveBtn.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        if (GameManager.instance.isLevelCompleted)
        {            
            scoreText.text = $"Score : {0}";
            GameManager.instance.dataStore.ResetGameData();
            //loadBtn.interactable = false;
            GameManager.instance.isLevelCompleted = false;  
        }
        else
        {
            GameManager.instance.dataStore.IsGameDataSaved = true;
            loadBtn.interactable = true;
            yield return new WaitUntil(() => GameManager.instance.dataStore.IsGameSaved);            
        }

        //Restart GamePlay
        //GameManager.instance.dataStore.CheckIfGameDataAvailable();
        if(GameManager.instance.dataStore.IsGameDataSaved)
            loadBtn.interactable = true;
        else
            loadBtn.interactable = false;

        GameManager.instance.RestartGame();
    }

    public void DisplayScore()
    {
        scoreText.text = $"Score : {GameManager.instance.Score}";
    }
}
