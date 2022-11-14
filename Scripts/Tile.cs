using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Tile : MonoBehaviour
{
    TextMeshProUGUI coordinate;
    BoardManager bm;
    public int id=0;
    public void SetImage(Sprite material)
    {
        GetComponent<Image>().sprite = material;
    }    
    private void Start()
    {
        bm = BoardManager._instance;
        //coordinate = GameObject.FindGameObjectWithTag("Coordinate").GetComponent<TextMeshProUGUI>();
    }
    private void OnMouseOver()
    {
        /*coordinate.text = gameObject.name+$" || {(int)Enum.Parse(typeof(Enums.coordinates),gameObject.name.Substring(0,1)) }" +
            $"{(int.Parse(gameObject.name.Substring(1,1))-1)}";*/
    }
    private void OnMouseDown()
    {
        print(gameObject.name);

    }
}