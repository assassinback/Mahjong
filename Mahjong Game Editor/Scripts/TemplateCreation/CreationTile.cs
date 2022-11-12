// Author: Oleksii Stepanov

using System.Collections.Generic;
using UnityEngine;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class that holds information of the layers 
    /// </summary>
    internal class CreationTile : MonoBehaviour
    {
        /// <summary>
        /// Row of the field
        /// </summary>
        internal string row = "";

        /// <summary>
        /// Place in the row
        /// </summary>
        internal string place = "";

        /// <summary>
        /// Nearbly ids of the tile 
        /// </summary>
        internal List<string> closeIds = new List<string>();

        /// <summary>
        /// Id of the tile
        /// </summary>
        internal string id = "";

        /// <summary>
        /// Layer 1 status
        /// </summary>
        internal string layer1Status = "empty";

        /// <summary>
        /// Layer 2 status
        /// </summary>
        internal string layer2Status = "empty";

        /// <summary>
        /// Layer 3 status
        /// </summary>
        internal string layer3Status = "empty";

        /// <summary>
        /// Layer 4 status
        /// </summary>
        internal string layer4Status = "empty";

        /// <summary>
        /// Layer 5 status
        /// </summary>
        internal string layer5Status = "empty";

        /// <summary>
        /// Sprite Renderer variable
        /// </summary>
        private SpriteRenderer sprite;

        /// <summary>
        /// Block status 
        /// </summary>
        private bool block = false;

        private void Awake()
        {
            /// id equels gameObject name
            id = gameObject.name;
            
            /// Assigns sprite renderer to variable
            sprite = GetComponent<SpriteRenderer>();

            /// Array of the splited by " " 
            string[] splitArray = id.Split(char.Parse(" "));
            
            /// Row equels to splitArray element number 1
            row = splitArray[0];

            /// place  equels to splitArray element number 1
            place = splitArray[1];

            ///
            FindCloseIDs();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FindCloseIDs()
        {
            ///
            int rowInt = int.Parse(row);
            
            ///
            int placeInt = int.Parse(place);

            ///
            int row1 = rowInt - 1;
            
            ///
            int row2 = rowInt;
            
            ///
            int row3 = rowInt + 1;

            ///
            int place1 = placeInt - 1;

            ///
            int place2 = placeInt;

            ///
            int place3 = placeInt + 1;

            ///
            string row1id1 = row1.ToString() + " " + place1.ToString();

            ///
            string row1id2 = row1.ToString() + " " + place2.ToString();

            ///
            string row1id3 = row1.ToString() + " " + place3.ToString();

            ///
            string row2id1 = row2.ToString() + " " + place1.ToString();

            ///
            string row2id3 = row2.ToString() + " " + place3.ToString();


            ///
            string row3id1 = row3.ToString() + " " + place1.ToString();

            ///
            string row3id2 = row3.ToString() + " " + place2.ToString();

            ///
            string row3id3 = row3.ToString() + " " + place3.ToString();

            ///
            closeIds.Add(row1id1);

            ///
            closeIds.Add(row1id2);

            ///
            closeIds.Add(row1id3);

            ///
            closeIds.Add(row2id1);

            ///
            closeIds.Add(row2id3);

            ///
            closeIds.Add(row3id1);

            ///
            closeIds.Add(row3id2);

            ///
            closeIds.Add(row3id3);
        }

        private void OnMouseDown()
        {
            ///
            if (layer5Status == "empty")
            {
                ///
                if (!block)
                {
                    ///
                    SetLayerStatus();

                    ///
                    SetColor();

                    ///
                    CreationTiles.Instance.UnblockAll();

                    ///
                    UICreation.Instance.AddTileCount();

                    ///
                    block = true;

                    ///
                    BlockNeably();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void BlockNeably()
        {
            ///
            for (int i = 0; i < closeIds.Count; i++)
            {
                ///
                CreationTiles.Instance.BlockTile(closeIds[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void BlockTile(bool value)
        {
            ///
            block = value;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetLayerStatus()
        {
            ///
            if (layer1Status == "empty")
            {
                ///
                layer1Status = "occupied";

                ///
                CreationTiles.Instance.AddToSequence(id + " " + "1");

                ///
                UpdateAllCloseTilesLayer(1, "blocked");
            }
            else if (layer2Status == "empty")
            {
                ///
                layer2Status = "occupied";

                ///
                CreationTiles.Instance.AddToSequence(id + " " + "2");

                ///
                UpdateAllCloseTilesLayer(2, "blocked");
            }
            else if (layer3Status == "empty")
            {
                ///
                layer3Status = "occupied";

                ///
                CreationTiles.Instance.AddToSequence(id + " " + "3");
                
                ///
                UpdateAllCloseTilesLayer(3, "blocked");
            }
            else if (layer4Status == "empty")
            {
                ///
                layer4Status = "occupied";

                ///
                CreationTiles.Instance.AddToSequence(id + " " + "4");

                ///
                UpdateAllCloseTilesLayer(4, "blocked");
            }
            else if (layer5Status == "empty")
            {
                ///
                layer5Status = "occupied";

                ///
                CreationTiles.Instance.AddToSequence(id + " " + "5");

                ///
                UpdateAllCloseTilesLayer(5, "blocked");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void DeleteTile()
        {
            ///
            if (layer5Status == "occupied")
            {
                ///
                layer5Status = "empty";

                ///
                UpdateAllCloseTilesLayer(5, "empty");
            }
            else if (layer4Status == "occupied")
            {
                ///
                layer4Status = "empty";

                ///
                UpdateAllCloseTilesLayer(4, "empty");
            }
            else if (layer3Status == "occupied")
            {
                ///
                layer3Status = "empty";

                ///
                UpdateAllCloseTilesLayer(3, "empty");
            }
            else if (layer2Status == "occupied")
            {
                ///
                layer2Status = "empty";

                ///
                UpdateAllCloseTilesLayer(2, "empty");
            }
            else if (layer1Status == "occupied")
            {
                ///
                layer1Status = "empty";

                ///
                UpdateAllCloseTilesLayer(1, "empty");
            }

            ///
            CreationTiles.Instance.UnblockAll();

            ///
            SetColor();

            ///
            UICreation.Instance.RemoveTileCount();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void UpdateAllCloseTilesLayer(int layerNumber, string status)
        {
            ///
            for (int i = 0; i < closeIds.Count; i++)
            {
                ///
                CreationTiles.Instance.UpdateTile(closeIds[i], layerNumber, status);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetLayer1Status(string value)
        {
            ///
            layer1Status = value;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetLayer2Status(string value)
        {
            ///
            layer2Status = value;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetLayer3Status(string value)
        {
            ///
            layer3Status = value;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetLayer4Status(string value)
        {
            ///
            layer4Status = value;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetLayer5Status(string value)
        {
            ///
            layer5Status = value;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SetColor()
        {
            ///
            if (layer1Status == "empty")
            {
                ///
                sprite.color = ColorManager.Instance.black;
            }

            ///
            if (layer1Status == "occupied")
            {
                ///
                sprite.color = ColorManager.Instance.red;
            }

            ///
            if (layer1Status == "blocked")
            {
                ///
                sprite.color = ColorManager.Instance.lightRed;
            }

            ///
            if (layer2Status == "occupied")
            {
                ///
                sprite.color = ColorManager.Instance.blue;
            }

            ///
            if (layer2Status == "blocked")
            {
                ///
                sprite.color = ColorManager.Instance.lightBlue;
            }

            ///
            if (layer3Status == "occupied")
            {
                ///
                sprite.color = ColorManager.Instance.orange;
            }

            ///
            if (layer3Status == "blocked")
            {
                ///
                sprite.color = ColorManager.Instance.lightOrange;
            }

            ///
            if (layer4Status == "occupied")
            {
                ///
                sprite.color = ColorManager.Instance.pink;
            }

            ///
            if (layer4Status == "blocked")
            {
                ///
                sprite.color = ColorManager.Instance.lightPink;
            }

            ///
            if (layer5Status == "occupied")
            {
                ///
                sprite.color = ColorManager.Instance.green;
            }

            ///
            if (layer5Status == "blocked")
            {
                ///
                sprite.color = ColorManager.Instance.lightGreen;
            }
        }

        private void OnMouseEnter()
        {
            ///
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        private void OnMouseExit()
        {
            ///
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}