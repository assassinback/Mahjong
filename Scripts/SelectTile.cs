using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class SelectTile : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(selectTile);    
    }
    private void Start()
    {
        id=GetComponent<Tile>().id;
        //GameManager._instance.SetHints(1000);
    }
    private void selectTile()
    {
        UIManager._instance.AddToStack();
        if (TileManager._instance.selectTiles.Count >= 10)
        {

        }
        if (!TileManager._instance.selectTiles.Contains(this))
        {
            TileManager._instance.selectedCount++;
            TileManager._instance.selectTiles.Add(this);
            UIManager._instance.AddToStack();
            int j = 0;
            for(int i=0;i< TileManager._instance.selectTiles.Count;i++)
            {
                if(TileManager._instance.selectTiles[i].id==id)
                {
                    j++;
                }
                if(j==4)
                {
                    TileManager._instance.selectedCount = 0;
                    foreach(SelectTile tile in TileManager._instance.selectTiles)
                    {
                        if (tile.id == id)
                        {
                            //TileManager._instance.selectTiles.Remove(tile);
                            //Destroy(tile.gameObject);
                            tile.gameObject.SetActive(false);
                            //BoardManager._instance.ActivateLayer(0);
                            BoardManager._instance.RemoveEmptyRows();
                            TileManager._instance.originalTime += 2f;
                            if(TileManager._instance.originalTime>TileManager._instance.currentLevelInfo.levelTime)
                            {
                                TileManager._instance.originalTime = TileManager._instance.currentLevelInfo.levelTime;
                            }
                            TileManager._instance.topLayerTiles = BoardManager._instance.GetTopLayerTiles();
                            //StartCoroutine(CheckDestroy());



                        }
                    }
                    TileManager._instance.RefreshSelectTiles(id);
                    UIManager._instance.AddToStack();
                    TileManager._instance.RefreshCards(id);
                    //Destroy(this.gameObject);
                    return;
                }
            }
        }
        
    }
    IEnumerator CheckDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        BoardManager._instance.RemoveEmptyRows();
    }
}
