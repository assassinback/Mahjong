using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BoardManager : MonoBehaviour
{
    public static BoardManager _instance;

    public List<Sprite> materials;
    public List<int> doneSprite;
    /*    public Layer[] layer;
    */
    [field: SerializeField] public Table table;
    public Color deselectColor;
    public Color selectColor;
    public bool isStaked;
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
        /*        table = new Table(table.layers);
        */
        doneSprite = new List<int>();
        GenerateBoard();

    }
    public void GenerateBoard()
    {
        GameObject Cube = Resources.Load<GameObject>("Tile");
        
        

        //Material mat = white;
        Transform parrent = transform.GetChild(0);
        //  Setting Tiles
        Vector3 firstTrans = new Vector3();
        int i = 0;
        for (int k = 0; k < table.layers.Length; k++)
        {
            firstTrans = new Vector3(-337, 310, 0);
            

            float x = ((6f - table.layers[k].columns.Length) / 2f);
            //float x = Mathf.Ceil((6.0f - table.layers[k].columns[i].rows.Length) / 2f);
            //print($" val = {x} and length = {table.layers[k].columns[i].rows.Length}");
            firstTrans.x += x * (Cube.GetComponent<RectTransform>().localScale.x + 140);
            

            for (i = 0; i < table.layers[k].columns.Length; i++)
            {
                firstTrans.y  = 310;
                float y = (6f - table.layers[k].columns[i].rows.Length) / 2f;
                firstTrans.y += y * (Cube.GetComponent<RectTransform>().localScale.y - 140);





                //print($"{table.layers[k].columns[i].rows.Length}");
                //int rGap = 8 / table.layers[k].columns[i].rows.Length;
                for (int j = 0; j < table.layers[k].columns[i].rows.Length; j++)
                {

                    GameObject tile = Instantiate(Cube, transform.position, Quaternion.identity);
                    //tile.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    table.layers[k].columns[i].rows[j].tile = tile;
                    table.layers[k].columns[i].rows[j].tileScript = tile.GetComponent<Tile>();
                    table.layers[k].columns[i].rows[j].tile.GetComponent<RectTransform>().position = new Vector3(table.layers[k].columns[i].rows[j].tile.GetComponent<RectTransform>().position.x, table.layers[k].columns[i].rows[j].tile.GetComponent<RectTransform>().position.y,50);
                    //Debug.Log("f");
                    //grid.y[i].x[j] = tile;
                    tile.GetComponent<RectTransform>().sizeDelta = new Vector2(123, 127);
                    tile.transform.SetParent(parrent);
                    //tile.GetComponent<MeshRenderer>().material = mat;
                    //firstTrans.y -= 309;
                    //firstTrans.x += Cube.GetComponent<RectTransform>().localScale.x + 140;
                    tile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    tile.gameObject.name = $"layer = {k} , col = {i} , row {j}";

                    tile.transform.GetComponent<RectTransform>().anchoredPosition = firstTrans;
                    firstTrans.y += Cube.GetComponent<RectTransform>().localScale.y - 140;
                }
                firstTrans.x += (Cube.GetComponent<RectTransform>().localScale.x + 140);

               
            }
            i = 0;
            firstTrans.z -= Cube.transform.localScale.z;
            Vector3 tempZPos = firstTrans;
            firstTrans.z = tempZPos.z;
        }
        GiveIds();
        ActivateLayer(GetTopLayer());
    }
    public int random()
    {
        return UnityEngine.Random.Range(0, materials.Count);
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
            doneSprite.Clear();
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
                tiles[temp].matchId = materialToUse;
                tiles.RemoveAt(temp);
                //Debug.Log(materialToUse);

                index++;
                if (index == 4)
                {
                    id++;
                    index = 0;
                    doneSprite.Add(materialToUse);

                reselect:

                    materialToUse = random();
                    if(doneSprite.Count==9)
                    {
                        goto dontreselect;
                    }
                    if(doneSprite.Contains(materialToUse))
                    {
                        goto reselect;
                    }
                dontreselect:
                    continue;
                    
                    
                    
                    //materials.Remove(materials[materialToUse]);
                    //materialToUse = random();
                    
                }
            }

        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateActivatedTiles();
        }
    }
    public void ActivateLayer(int layerNumber)
    {
        try
        {
            foreach (var item in table.layers)
            {
                foreach (var item2 in item.columns)
                {
                    foreach (var item3 in item2.rows)
                    {
                        item3.tile.GetComponent<Image>().color = deselectColor;
                        item3.tile.GetComponent<Button>().enabled = false;
                        item3.tileScript.isActivated = false;
                        item3.tileScript.isStacked = true;
                    }
                }
            }
            foreach (var item in table.layers[layerNumber].columns)
            {
                foreach (var go in item.rows)
                {
                    var color = go.tile.GetComponent<Image>().color;
                    
                    go.tile.GetComponent<Image>().color = selectColor;
                    go.tileScript.isActivated = true;
                    go.tile.GetComponent<Button>().enabled = true;
                    go.tileScript.isStacked = false;


                }
            }
        }
        catch(System.Exception)
        {

        }
        
    }

    public int GetTopLayer()
    {
        
        //for(int i=0;i<table.layers.Length;i++)
        //{
        //    if(table.layers[i]==null)
        //    {
        //        return i-1;
        //    }
                
        //}
        
        return table.layers.Length-1;
    }
    public int GetSecondTopLayer()
    {
        
        return table.layers.Length-2;
    }
    public void RemoveTopLayer()
    {
        try
        {
            List<Layer> temp = table.layers.ToList();
            temp.RemoveAt(GetTopLayer());
            table.layers = temp.ToArray();
        }
        catch(System.Exception)
        {

        }

    }
    public void RemoveEmptyRows()
    {
        try
        {
            int n = 0;
            //Debug.Log(GetTopLayer());

            for (int i = 0; i < table.layers[GetTopLayer()].columns.Length; i++)
            {

                for (int j = 0; j < table.layers[GetTopLayer()].columns[i].rows.Length; j++)
                {

                    if (!table.layers[GetTopLayer()].columns[i].rows[j].tile.activeSelf)
                    {
                        //table.layers[GetTopLayer()] = null;
                        n++;
                        //print(n);
                        //ActivateLayer(GetTopLayer()-1);
                    }
                }
            }
            
            if (n >= table.layers[GetTopLayer()].columns.Length * table.layers[GetTopLayer()].columns[0].rows.Length)
            {
                RemoveTopLayer();
                ActivateLayer(GetTopLayer());
            }
        }
        catch(System.Exception)
        {

        }
       
            

        
    }
    public void ActivateActivatedTiles()
    {

        foreach (var ii in table.layers[GetSecondTopLayer()].columns)
        {
            foreach (var jj in ii.rows)
            {
                if (jj.tileScript.isActivated)
                {
                    jj.tileScript.isStacked = false;
                    jj.tileScript.GetComponent<Button>().enabled = true;

                }

            }
        }
        foreach (var i in table.layers[GetTopLayer()].columns)
        {

            foreach (var j in i.rows)
            {

                foreach (var ii in table.layers[GetSecondTopLayer()].columns)
                {
                    foreach (var jj in ii.rows)
                    {
                        if (jj.tileScript.isActivated && !jj.tileScript.isStacked)
                        {


                            //print($"checking......{j.tile.gameObject.name} , {jj.tile.gameObject.name}  {Vector2.Distance(j.tileScript.rectTransform.position, jj.tileScript.rectTransform.position)}");
                            if (Vector2.Distance(j.tileScript.rectTransform.position, jj.tileScript.rectTransform.position) < 75
                                /*|| Vector2.Distance(j.tileScript.rectTransform.position, jj.tileScript.rectTransform.position) < 71*/ )
                            {
                                //print($"false......{j.tile.gameObject.name} , {jj.tile.gameObject.name}  {Vector2.Distance(j.tileScript.rectTransform.position, jj.tileScript.rectTransform.position)}");

                                jj.tileScript.isStacked = true;
                                jj.tileScript.GetComponent<Button>().enabled = false;


                            }

                        }
                    }
                }









            }
        }
    }

    public int GetTileCount(int layerId)
    {
        return table.layers[layerId].columns.Length * table.layers[layerId].columns[0].rows.Length;
    }
    public List<GameObject> GetTopLayerTiles()
    {
        List<GameObject> temp = new List<GameObject>();
        try
        {
            
            int topLayer = GetTopLayer();
            for (int i = 0; i < table.layers[GetTopLayer()].columns.Length; i++)
            {
                for (int j = 0; j < table.layers[GetTopLayer()].columns[i].rows.Length; j++)
                {
                    temp.Add(table.layers[GetTopLayer()].columns[i].rows[j].tile);
                }
            }
            
        }
        catch(System.Exception)
        {

        }
        return temp;
    }
    public void ActivateTilesInLayer(int layer,int amount)
    {
        if (layer<0)
        {
            return;
        }
        int temp = amount;
        for (int i = 0; i < table.layers[layer].columns.Length; i++)
        {
            for (int j = 0; j < table.layers[layer].columns[i].rows.Length; j++)
            {
                if (!table.layers[layer].columns[i].rows[j].tileScript.isActivated)
                {
                    table.layers[layer].columns[i].rows[j].tileScript.image.color = selectColor;
                    table.layers[layer].columns[i].rows[j].tileScript.isActivated = true;
                    table.layers[layer].columns[i].rows[j].tileScript.GetComponent<Button>().enabled = true;
                    temp -= 1;
                }
                if (temp<=0)
                {
                    return;
                }
            }
        }
    }
}


[System.Serializable]
public class Row
{
    public Row()
    {
    }
    [field: SerializeField] public GameObject tile;
    [field: SerializeField] public Tile tileScript;
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

    public Layer(int columnAmount, int rowAmount)
    {
        columns = new Column[columnAmount];
        for (int i = 0; i < columnAmount; i++)
        {
            columns[i] = new Column(rowAmount);
        }
    }

    [field: SerializeField] public Column[] columns;
    
}
[System.Serializable]
public class Table
{
    [field: SerializeField] public Layer[] layers;
    [field: SerializeField] public string levelName;
    public Table(int layerAmount, int columnAmount, int rowAmount)
    {
        layers = new Layer[layerAmount];
        for (int i = 0; i < layerAmount; i++)
        {
            layers[i] = new Layer(columnAmount, rowAmount);
        }
    }
    public int GetTopLayerTiles()
    {
        return 1;
    }
    public Table(Layer[] layers)
    {
        this.layers = layers;
    }
    public Table()
    {

    }
}