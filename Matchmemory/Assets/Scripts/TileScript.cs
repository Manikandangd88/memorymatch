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
    private bool active = true;
    private Button tileButton;

    public Image TileImage = null;

    public int ID { get => id; set => id = value; }
    public bool ActiveStatus { get => active; set => active = value; }


    private void Awake()
    {
        tileButton = GetComponent<Button>();
        tileButton.onClick.AddListener(OnTileTouched);

        GameManager.DestroyTiles += KillMyself;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetTileProperties(Sprite img, int tileid)
    public void SetTileProperties(Sprite img)
    {
        //id = tileid;
        //gameObject.name = id.ToString();
        TileImage.sprite = img;
    }

    public void OnTileTouched()
    {
        //Flip Tile
        if (!Flipped) 
        {
            Flipped = true;
            //GameManager.instance.FlippedTiles.Add(this);
            tileButton.interactable = false;

            transform.DOLocalRotate(new Vector2(0,180f), flipSpeed, RotateMode.Fast)
                .OnComplete(OnTileFlipped);
        }
        else
        {
            //OnTileCanceled();
        }

        //Check if Tiles Match

    }

    public void OnTileFlipped()
    {
        GameManager.instance.AddToPair(this);
    }

    public void OnTileMatch()
    {       
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;

        this.enabled = false;
        ActiveStatus = false;

        GameManager.instance.CheckLevelcompleted();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnTileCanceled()
    {
        GameManager.instance.CancelPair();
        ResetTile();
    }

    public void ResetTile()
    {
        Flipped = false;
        transform.DOLocalRotate(new Vector2(0, 0), flipSpeed, RotateMode.Fast)
            .OnComplete(() => tileButton.interactable = true);
    }

    public void KillMyself()
    {
        Destroy(gameObject);
    }

}
