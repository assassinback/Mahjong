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
    }
    private void selectTile()
    {
        if(!TileManager._instance.selectTiles.Contains(this))
        {
            TileManager._instance.selectedCount++;
            TileManager._instance.selectTiles.Add(this);
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
                            Destroy(tile.gameObject);


                            
                        }
                    }
                    TileManager._instance.RefreshSelectTiles(id);
                    TileManager._instance.RefreshCards(id);
                    //Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
}
