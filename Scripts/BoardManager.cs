using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BoardManager : MonoBehaviour
{
    public static BoardManager _instance;

    public List<Sprite> materials;
    public int rows, columns, layers;
    [field: SerializeField]public Table table;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        //StartGenerating();


    }
    public void StartGenerating()
    {
        table = new Table(layers, columns, rows);

        GenerateBoard();

    }
    public void GenerateBoard()
    {
        if (Mathf.Pow( columns*rows,layers)%4 != 0)
        {
            return;
        }
        //Material black = Resources.Load<Material>("Black"), white = Resources.Load<Material>("White");
        GameObject Cube = Resources.Load<GameObject>("Tile");
        //Material mat = white;
        Transform parrent = transform.GetChild(0);
        Vector3 firstTrans = new Vector3(-337,300,0);
        //  Setting Tiles
        for (int k = 0; k < table.layers.Length; k++)
        {
            for (int i = 0; i < table.layers[k].columns.Length; i++)
            {
                //print($"{table.layers[k].columns[i].rows.Length}");

                for (int j = 0; j < table.layers[k].columns[i].rows.Length; j++)
                {
                    GameObject tile = Instantiate(Cube, transform.position, Quaternion.identity);
                    table.layers[k].columns[i].rows[j].tile = tile;
                    //grid.y[i].x[j] = tile;
                    tile.GetComponent<RectTransform>().sizeDelta = new Vector2(123,127);
                    tile.transform.SetParent(parrent);
                    
                    //tile.GetComponent<MeshRenderer>().material = mat;
                    tile.transform.GetComponent<RectTransform>().anchoredPosition = firstTrans;
                    firstTrans.x += 130;
                    //firstTrans.y -= 309;
                    firstTrans.x += Cube.GetComponent<RectTransform>().localScale.x;


                    tile.gameObject.name = $"layer = {k} , col = {i} , row {j}";
                }
                firstTrans.x = parrent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
                firstTrans.y -= 145;
            }
            firstTrans.z -= Cube.transform.localScale.z;
            var tempZPos = firstTrans;
            firstTrans = parrent.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
            firstTrans.z = tempZPos.z;
        }
        GiveIds();
    }
    public int random()
    {
        return UnityEngine.Random.Range(0, materials.Count - 1);
    }
    void GiveIds()
    {
        int id = 0;
        int index = 0;
        //rand
        int materialToUse = random();
        List<Tile> tiles = new List<Tile>();
        foreach (var item in table.layers)
        {
            foreach (var c in item.columns)
            {
                foreach (var r in c.rows)
                {
                    tiles.Add(r.tile.GetComponent<Tile>());


                    
                }
            }
            while (tiles.Count > 0)
            {

                int temp = UnityEngine.Random.Range(0, tiles.Count);
                //Debug.Log(tiles[temp]);
                tiles[temp].id = id;
                tiles[temp].gameObject.name += $" ,id = {id}";
                tiles[temp].SetImage(materials[materialToUse]);
                
                tiles.RemoveAt(temp);
                //Debug.Log(materialToUse);
                
                index++;
                if (index == 4)
                {
                    id++;
                    index = 0;
                    //materials.Remove(materials[materialToUse]);
                    materialToUse = random();
                }
            }

        }

    }

}


[System.Serializable]
public class Row
{
    public Row(){
    }
   [field: SerializeField] public GameObject tile;
}
[System.Serializable]

public class Column
{
    public Column(int rowAmount)
    {
        rows = new Row[rowAmount];
        for (int i = 0; i < rowAmount; i++)
        {
            rows[i] = new Row();
        }
    }
   [field: SerializeField] public Row[] rows;
}
[System.Serializable]

public class Layer
{

    public Layer(int columnAmount,int rowAmount)
    {
        columns = new Column[columnAmount];
        for (int i = 0; i < columnAmount; i++)
        {
            columns[i] = new Column(rowAmount);
        }
    }

   [field: SerializeField]public Column[] columns;
}
[System.Serializable]
public class Table
{
    [field:SerializeField]public Layer[] layers;
    public Table(int layerAmount,int columnAmount,int rowAmount)
    {
        layers = new Layer[layerAmount];
        for (int i = 0; i < layerAmount; i++)
        {
            layers[i] = new Layer(columnAmount,rowAmount);
        }
    }
}