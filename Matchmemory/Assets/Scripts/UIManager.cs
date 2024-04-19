using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button submit;

    public GameObject FetchRowsAndColumnsUI = null;

    [SerializeField] private TMP_InputField rows = null;
    [SerializeField] private TMP_InputField columns = null;



    private void Awake()
    {
        submit.onClick.AddListener(OnSubmitPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
