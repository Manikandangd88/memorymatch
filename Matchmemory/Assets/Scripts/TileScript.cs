using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TileScript : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float flipSpeed = 0;
    private bool Flipped = false;

    private Button tileButton;

    public int ID { get => id; set => id = value; }

    private void Awake()
    {
        tileButton = GetComponent<Button>();
        tileButton.onClick.AddListener(OnTileTouched);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTileTouched()
    {
        //Flip Tile
        if (!Flipped) 
        {
            Flipped = true;
            transform.DOLocalRotate(new Vector2(0,180f), flipSpeed, RotateMode.Fast)
                .OnComplete(()=> GameManager.instance.CompareTiles());
            GameManager.instance.FlippedTiles.Add(this);
        }
        else
        {
            Flipped = false;
            transform.DOLocalRotate(new Vector2(0, 0), flipSpeed, RotateMode.Fast);
        }

        //Check if Tiles Match

    }
}
