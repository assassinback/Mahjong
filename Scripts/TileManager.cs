using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager _instance;
    public List<Transform> cards;
    public int selectedCount;
    public List<SelectTile> selectTiles;
    private void Awake()
    {
        _instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        cards = new List<Transform>();
        cards.AddRange(this.GetComponentsInChildren<Transform>());
        cards.Remove(transform);
        foreach (Transform card in cards)
        {
            card.gameObject.AddComponent<SelectTile>();
        }
    }
    public void RefreshSelectTiles(int id)
    {
        for (int k = 0; k < selectTiles.Count; k++)
        {
            if (id==selectTiles[k].id)
            {
                selectTiles.RemoveAt(k);
                k = -1;
            }
        }
        //Debug.Log("here");
        //selectTiles.RemoveAll(x => x== null);
    }
    public void RefreshCards(int id)
    {
        for (int k = 0; k < cards.Count; k++)
        {
            if (id == cards[k].GetComponent<Tile>().id)
            {
                cards.RemoveAt(k);
                k = -1;
            }
        }
        //cards = cards.Where(item => item != null).ToList();
    }
}
