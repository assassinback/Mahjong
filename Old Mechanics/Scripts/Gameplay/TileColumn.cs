// Author: Oleksii Stepanov

using System.Collections.Generic;
using UnityEngine;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class manages column of tiles 
    /// </summary>
    internal class TileColumn : MonoBehaviour
    {
        /// <summary>
        /// Id of the column
        /// </summary>
        internal string id = "";

        /// <summary>
        /// Layer 1 of the column
        /// </summary>
        private GameObject layer1;

        /// <summary>
        /// Layer 2 of the column
        /// </summary>
        private GameObject layer2;

        /// <summary>
        /// Layer 3 of the column
        /// </summary>
        private GameObject layer3;

        /// <summary>
        /// Layer 4 of the column
        /// </summary>
        private GameObject layer4;

        /// <summary>
        /// Layer 5 of the column
        /// </summary>
        private GameObject layer5;

        /// <summary>
        /// Layer 1 active status
        /// </summary>
        internal bool layer1Active = false;

        /// <summary>
        /// Layer 2 active status
        /// </summary>
        internal bool layer2Active = false;

        /// <summary>
        /// Layer 3 active status
        /// </summary>
        internal bool layer3Active = false;

        /// <summary>
        /// Layer 4 active status
        /// </summary>
        internal bool layer4Active = false;

        /// <summary>
        /// Layer 5 active status
        /// </summary>
        internal bool layer5Active = false;

        /// <summary>
        /// Layer 1 used status
        /// </summary>
        private bool layer1Used = false;

        /// <summary>
        /// Layer 2 used status
        /// </summary>
        private bool layer2Used = false;

        /// <summary>
        /// Layer 3 used status
        /// </summary>
        private bool layer3Used = false;

        /// <summary>
        /// Layer 4 used status
        /// </summary>
        private bool layer4Used = false;

        /// <summary>
        /// Layer 5 used status
        /// </summary>
        private bool layer5Used = false;

        /// <summary>
        /// List of id of the closest columns
        /// </summary>
        private List<string> closeIds = new List<string>();

        /// <summary>
        /// List of closest columns
        /// </summary>
        private List<TileColumn> closeTileColumns = new List<TileColumn>();

        private void Awake()
        {
            /// Sets the id of the column by parents name and gameObjects name
            id = transform.parent.name + " " + gameObject.name;

            /// Assigns layer 1 gameObject to the variable 
            layer1 = transform.GetChild(0).gameObject;

            /// Assigns layer 2 gameObject to the variable
            layer2 = transform.GetChild(1).gameObject;

            /// Assigns layer 3 gameObject to the variable
            layer3 = transform.GetChild(2).gameObject;

            /// Assigns layer 4 gameObject to the variable
            layer4 = transform.GetChild(3).gameObject;

            /// Assigns layer 5 gameObject to the variable
            layer5 = transform.GetChild(4).gameObject;

            /// Sets the matrix name of the layer of the TileLayer class
            layer1.GetComponent<TileLayer>().layerMatrixName = id + " " + "1";

            /// Sets the matrix name of the layer of the TileLayer class
            layer2.GetComponent<TileLayer>().layerMatrixName = id + " " + "2";

            /// Sets the matrix name of the layer of the TileLayer class
            layer3.GetComponent<TileLayer>().layerMatrixName = id + " " + "3";

            /// Sets the matrix name of the layer of the TileLayer class
            layer4.GetComponent<TileLayer>().layerMatrixName = id + " " + "4";

            /// Sets the matrix name of the layer of the TileLayer class
            layer5.GetComponent<TileLayer>().layerMatrixName = id + " " + "5";

            /// Sets the sorting layer name for layer 1
            layer1.GetComponent<SpriteRenderer>().sortingLayerName = "Tile1";

            /// Sets the sorting layer name for layer 2
            layer2.GetComponent<SpriteRenderer>().sortingLayerName = "Tile2";

            /// Sets the sorting layer name for layer 3
            layer3.GetComponent<SpriteRenderer>().sortingLayerName = "Tile3";

            /// Sets the sorting layer name for layer 4
            layer4.GetComponent<SpriteRenderer>().sortingLayerName = "Tile4";

            /// Sets the sorting layer name for layer 5
            layer5.GetComponent<SpriteRenderer>().sortingLayerName = "Tile5";

            /// Sets the order of the sorting layer for layer 1
            layer1.GetComponent<SpriteRenderer>().sortingOrder = (int.Parse(transform.name) * 100 - int.Parse(transform.parent.name) * 100) * -1;

            /// Sets the order of the sorting layer for layer 2
            layer2.GetComponent<SpriteRenderer>().sortingOrder = (int.Parse(transform.name) * 100 - int.Parse(transform.parent.name) * 100) * -1;

            /// Sets the order of the sorting layer for layer 3
            layer3.GetComponent<SpriteRenderer>().sortingOrder = (int.Parse(transform.name) * 100 - int.Parse(transform.parent.name) * 100) * -1;

            /// Sets the order of the sorting layer for layer 4
            layer4.GetComponent<SpriteRenderer>().sortingOrder = (int.Parse(transform.name) * 100 - int.Parse(transform.parent.name) * 100) * -1;

            /// Sets the order of the sorting layer for layer 5
            layer5.GetComponent<SpriteRenderer>().sortingOrder = (int.Parse(transform.name) * 100 - int.Parse(transform.parent.name) * 100) * -1;
        }

        private void Start()
        {
            /// Finds ids of the closest columns
            FindCloseIDs();

            /// Finds ids beyound closest columns
            FindCloseIDsAdditional();
        }

        /// <summary>
        /// Resets all information of the column
        /// </summary>
        internal void ResetAll()
        {
            /// Sets the layer 1 active status to false
            layer1Active = false;

            /// Sets the layer 2 active status to false
            layer2Active = false;

            /// Sets the layer 3 active status to false
            layer3Active = false;

            /// Sets the layer 4 active status to false
            layer4Active = false;

            /// Sets the layer 5 active status to false
            layer5Active = false;

            /// Sets the layer 1 used status to false
            layer1Used = false;

            /// Sets the layer 2 used status to false
            layer2Used = false;

            /// Sets the layer 3 used status to false
            layer3Used = false;

            /// Sets the layer 4 used status to false
            layer4Used = false;

            /// Sets the layer 5 used status to false
            layer5Used = false;

            /// Resets layer 1
            layer1.GetComponent<TileLayer>().ResetAfterCreation();

            /// Resets layer 2
            layer2.GetComponent<TileLayer>().ResetAfterCreation();

            /// Resets layer 3
            layer3.GetComponent<TileLayer>().ResetAfterCreation();

            /// Resets layer 4
            layer4.GetComponent<TileLayer>().ResetAfterCreation();

            /// Resets layer 5
            layer5.GetComponent<TileLayer>().ResetAfterCreation();

            /// Diactivates layer 1 gameObject
            layer1.gameObject.SetActive(false);

            /// Diactivates layer 2 gameObject
            layer2.gameObject.SetActive(false);

            /// Diactivates layer 3 gameObject
            layer3.gameObject.SetActive(false);

            /// Diactivates layer 4 gameObject
            layer4.gameObject.SetActive(false);

            /// Diactivates layer 5 gameObject
            layer5.gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the active status of the layer by layer name to false
        /// </summary>
        internal void DeactivateLayer(string layerName)
        {
            /// Check if layerName is equels to TileLayer1
            if (layerName == "TileLayer1")
            {
                /// Sets layer 1 active status to false
                layer1Active = false;
            }

            /// Check if layerName is equels to TileLayer2
            else if (layerName == "TileLayer2")
            {
                /// Sets layer 2 active status to false
                layer2Active = false;
            }

            /// Check if layerName is equels to TileLayer3
            else if (layerName == "TileLayer3")
            {
                /// Sets layer 3 active status to false
                layer3Active = false;
            }

            /// Check if layerName is equels to TileLayer4
            else if (layerName == "TileLayer4")
            {
                /// Sets layer 4 active status to false
                layer4Active = false;
            }

            /// Check if layerName is equels to TileLayer5
            else if (layerName == "TileLayer5")
            {
                /// Sets layer 5 active status to false
                layer5Active = false;
            }
        }

        /// <summary>
        /// Sets the active status of the layer by layer name to true
        /// </summary>
        internal void ActivateLayer(string layerName)
        {
            /// Check if layerName is equels to TileLayer1
            if (layerName == "TileLayer1")
            {
                /// Sets layer 1 active status to true
                layer1Active = true;
            }

            /// Check if layerName is equels to TileLayer2
            else if (layerName == "TileLayer2")
            {
                /// Sets layer 2 active status to true
                layer2Active = true;
            }

            /// Check if layerName is equels to TileLayer3
            else if (layerName == "TileLayer3")
            {
                /// Sets layer 3 active status to true
                layer3Active = true;
            }

            /// Check if layerName is equels to TileLayer4
            else if (layerName == "TileLayer4")
            {
                /// Sets layer 4 active status to true
                layer4Active = true;
            }

            /// Check if layerName is equels to TileLayer5
            else if (layerName == "TileLayer5")
            {
                /// Sets layer 5 active status to true
                layer5Active = true;
            }
        }

        /// <summary>
        /// Finds id of the closest columns
        /// </summary>
        private void FindCloseIDs()
        {
            /// Split the id 
            string[] splitArray = id.Split(char.Parse(" "));

            /// Row number
            string row = splitArray[0];

            /// Place number
            string place = splitArray[1];

            /// Parse row number to int
            int rowInt = int.Parse(row);

            /// Parse place number to int
            int placeInt = int.Parse(place);

            /// Sets the row number of closest row 
            int row1 = rowInt - 1;

            /// Sets the row number of this column
            int row2 = rowInt;

            /// Sets the row number of closest column
            int row3 = rowInt + 1;

            /// Sets the place of the closest columns
            int place1 = placeInt - 2;

            /// Sets the place of the closest columns
            int place3 = placeInt + 2;

            /// Id of the closest column 
            string row1id1 = row1.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row2id1 = row2.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row3id1 = row3.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row1id3 = row1.ToString() + " " + place3.ToString();

            /// Id of the closest column
            string row2id3 = row2.ToString() + " " + place3.ToString();

            /// Id of the closest column
            string row3id3 = row3.ToString() + " " + place3.ToString();

            /// Adds closest column id to the list 
            closeIds.Add(row1id1);

            /// Adds closest column id to the list 
            closeIds.Add(row2id1);

            /// Adds closest column id to the list 
            closeIds.Add(row3id1);

            /// Adds closest column id to the list 
            closeIds.Add(row1id3);

            /// Adds closest column id to the list 
            closeIds.Add(row2id3);

            /// Adds closest column id to the list 
            closeIds.Add(row3id3);

            /// Creates list of the closest columns layers
            FindCloseTiles();
        }

        /// <summary>
        /// Finds additional id of the closest columns
        /// </summary>
        private void FindCloseIDsAdditional()
        {
            /// Split the id 
            string[] splitArray = id.Split(char.Parse(" "));

            /// Row number
            string row = splitArray[0];

            /// Place number
            string place = splitArray[1];

            /// Parse row number to int
            int rowInt = int.Parse(row);

            /// Parse place number to int
            int placeInt = int.Parse(place);

            /// Sets the row number of closest column
            int row1 = rowInt - 1;

            /// Sets the row number of this column
            int row2 = rowInt;

            /// Sets the row number of closest column
            int row3 = rowInt + 1;

            /// Sets the place of the closest columns
            int place1 = placeInt - 1;

            /// Sets the place of the closest columns
            int place2 = placeInt;

            /// Sets the place of the closest columns
            int place3 = placeInt + 1;

            /// Id of the closest column
            string row1id1 = row1.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row1id2 = row1.ToString() + " " + place2.ToString();

            /// Id of the closest column
            string row1id3 = row1.ToString() + " " + place3.ToString();

            /// Id of the closest column
            string row2id1 = row2.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row2id3 = row2.ToString() + " " + place3.ToString();

            /// Id of the closest column
            string row3id1 = row3.ToString() + " " + place1.ToString();

            /// Id of the closest column
            string row3id2 = row3.ToString() + " " + place2.ToString();

            /// Id of the closest column
            string row3id3 = row3.ToString() + " " + place3.ToString();

            /// Adds closest column id to the list 
            closeIds.Add(row1id1);

            /// Adds closest column id to the list 
            closeIds.Add(row1id2);

            /// Adds closest column id to the list 
            closeIds.Add(row1id3);

            /// Adds closest column id to the list 
            closeIds.Add(row2id1);

            /// Adds closest column id to the list 
            closeIds.Add(row2id3);

            /// Adds closest column id to the list 
            closeIds.Add(row3id1);

            /// Adds closest column id to the list 
            closeIds.Add(row3id2);

            /// Adds closest column id to the list 
            closeIds.Add(row3id3);

            /// Creates list of the closest columns layers
            FindCloseTiles();
        }

        /// <summary>
        /// Creates a column list of the closest columns
        /// </summary>
        private void FindCloseTiles()
        {
            /// Runs loop 
            for (int i = 0; i < closeIds.Count; i++)
            {
                /// Adds tile column to the list
                closeTileColumns.Add(TemplateManager.Instance.GetTileColumn(closeIds[i]));
            }

            /// Clears ids
            closeIds.Clear();
        }

        /// <summary>
        /// Checks Click allowed status for all of the layers in the column and in the closest columns
        /// </summary>
        internal void SetClickAllow()
        {
            /// Sets click allowed status according to layer position in column
            CheckLayersOverlay();

            /// Sets click allowed status according to layer position of the layer in closest columns 
            CheckCloseColumns();

            /// Sets click allowed status according to layer position of the layer in closest columns 
            CheckUpperLayers();
        }

        /// <summary>
        /// Sets click allowed status according to layer position in column
        /// </summary>
        private void CheckLayersOverlay()
        {
            /// Activate click allowed status of the layer 1
            layer1.GetComponent<TileLayer>().SetClickAllowed(true);

            /// Activate click allowed status of the layer 2
            layer2.GetComponent<TileLayer>().SetClickAllowed(true);

            /// Activate click allowed status of the layer 3
            layer3.GetComponent<TileLayer>().SetClickAllowed(true);

            /// Activate click allowed status of the layer 4
            layer4.GetComponent<TileLayer>().SetClickAllowed(true);

            /// Activate click allowed status of the layer 5
            layer5.GetComponent<TileLayer>().SetClickAllowed(true);

            /// Checks if layer 5 is active 
            if (layer5Active)
            {
                /// Dictivate click allowed status of the layer 1
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 2
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 3
                layer3.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 4
                layer4.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 4 is active 
            if (layer4Active)
            {
                /// Dictivate click allowed status of the layer 1
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 2
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 3
                layer3.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 3 is active 
            if (layer3Active)
            {
                /// Dictivate click allowed status of the layer 1
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Dictivate click allowed status of the layer 2
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 2 is active 
            if (layer2Active)
            {
                /// Dictivate click allowed status of the layer 2
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);
            }
        }

        /// <summary>
        /// Sets click allowed status according to two layers how could block tile by the rules of Mahjong game
        /// </summary>
        internal void CheckCloseColumns()
        {
            /// Returns layer status by the index of column and layer number
            bool column1layer1 = GetLayerStatus(1, 1);

            /// Returns layer status by the index of column and layer number
            bool column1layer2 = GetLayerStatus(1, 2);

            /// Returns layer status by the index of column and layer number
            bool column1layer3 = GetLayerStatus(1, 3);

            /// Returns layer status by the index of column and layer number
            bool column1layer4 = GetLayerStatus(1, 4);

            /// Returns layer status by the index of column and layer number
            bool column1layer5 = GetLayerStatus(1, 5);

            /// Returns layer status by the index of column and layer number
            bool column2layer1 = GetLayerStatus(2, 1);

            /// Returns layer status by the index of column and layer number
            bool column2layer2 = GetLayerStatus(2, 2);

            /// Returns layer status by the index of column and layer number
            bool column2layer3 = GetLayerStatus(2, 3);

            /// Returns layer status by the index of column and layer number
            bool column2layer4 = GetLayerStatus(2, 4);

            /// Returns layer status by the index of column and layer number
            bool column2layer5 = GetLayerStatus(2, 5);

            /// Returns layer status by the index of column and layer number
            bool column3layer1 = GetLayerStatus(3, 1);

            /// Returns layer status by the index of column and layer number
            bool column3layer2 = GetLayerStatus(3, 2);

            /// Returns layer status by the index of column and layer number
            bool column3layer3 = GetLayerStatus(3, 3);

            /// Returns layer status by the index of column and layer number
            bool column3layer4 = GetLayerStatus(3, 4);

            /// Returns layer status by the index of column and layer number
            bool column3layer5 = GetLayerStatus(3, 5);

            /// Returns layer status by the index of column and layer number
            bool column4layer1 = GetLayerStatus(4, 1);

            /// Returns layer status by the index of column and layer number
            bool column4layer2 = GetLayerStatus(4, 2);

            /// Returns layer status by the index of column and layer number
            bool column4layer3 = GetLayerStatus(4, 3);

            /// Returns layer status by the index of column and layer number
            bool column4layer4 = GetLayerStatus(4, 4);

            /// Returns layer status by the index of column and layer number
            bool column4layer5 = GetLayerStatus(4, 5);

            /// Returns layer status by the index of column and layer number
            bool column5layer1 = GetLayerStatus(5, 1);

            /// Returns layer status by the index of column and layer number
            bool column5layer2 = GetLayerStatus(5, 2);

            /// Returns layer status by the index of column and layer number
            bool column5layer3 = GetLayerStatus(5, 3);

            /// Returns layer status by the index of column and layer number
            bool column5layer4 = GetLayerStatus(5, 4);

            /// Returns layer status by the index of column and layer number
            bool column5layer5 = GetLayerStatus(5, 5);

            /// Returns layer status by the index of column and layer number
            bool column6layer1 = GetLayerStatus(6, 1);

            /// Returns layer status by the index of column and layer number
            bool column6layer2 = GetLayerStatus(6, 2);

            /// Returns layer status by the index of column and layer number
            bool column6layer3 = GetLayerStatus(6, 3);

            /// Returns layer status by the index of column and layer number
            bool column6layer4 = GetLayerStatus(6, 4);

            /// Returns layer status by the index of column and layer number
            bool column6layer5 = GetLayerStatus(6, 5);

            /// Checks if layer 5 is active in the closest columns
            if (column1layer5 || column2layer5 || column3layer5)
            {
                /// Checks if layer 5 is active in the closest columns
                if (column4layer5 || column5layer5 || column6layer5)
                {
                    /// Sets click allowed status of the layer 1 to false 
                    layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 2 to false
                    layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 3 to false
                    layer3.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 4 to false
                    layer4.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 5 to false
                    layer5.GetComponent<TileLayer>().SetClickAllowed(false);
                }
            }

            /// Checks if layer 4 is active in the closest columns
            if (column1layer4 || column2layer4 || column3layer4)
            {
                /// Checks if layer 4 is active in the closest columns
                if (column4layer4 || column5layer4 || column6layer4)
                {
                    /// Sets click allowed status of the layer 1 to false
                    layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 2 to false
                    layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 3 to false
                    layer3.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 4 to false
                    layer4.GetComponent<TileLayer>().SetClickAllowed(false);
                }
            }

            /// Checks if layer 3 is active in the closest columns
            if (column1layer3 || column2layer3 || column3layer3)
            {
                /// Checks if layer 3 is active in the closest columns
                if (column4layer3 || column5layer3 || column6layer3)
                {
                    /// Sets click allowed status of the layer 1 to false
                    layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 2 to false
                    layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 3 to false
                    layer3.GetComponent<TileLayer>().SetClickAllowed(false);
                }
            }

            /// Checks if layer 2 is active in the closest columns
            if (column1layer2 || column2layer2 || column3layer2)
            {
                /// Checks if layer 2 is active in the closest columns
                if (column4layer2 || column5layer2 || column6layer2)
                {
                    /// Sets click allowed status of the layer 1 to false
                    layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                    /// Sets click allowed status of the layer 2 to false
                    layer2.GetComponent<TileLayer>().SetClickAllowed(false);
                }
            }

            /// Checks if layer 1 is active in the closest columns
            if (column1layer1 || column2layer1 || column3layer1)
            {
                /// Checks if layer 1 is active in the closest columns
                if (column4layer1 || column5layer1 || column6layer1)
                {
                    /// Sets click allowed status of the layer 1 to false
                    layer1.GetComponent<TileLayer>().SetClickAllowed(false);
                }
            }
        }

        /// <summary>
        /// Sets click allowed status according to layer position of the neably columns
        /// </summary>
        private void CheckUpperLayers()
        {
            /// Returns layer status by the index of column and layer number
            bool column1layer2 = GetLayerStatus(7, 2);

            /// Returns layer status by the index of column and layer number
            bool column1layer3 = GetLayerStatus(7, 3);

            /// Returns layer status by the index of column and layer number
            bool column1layer4 = GetLayerStatus(7, 4);

            /// Returns layer status by the index of column and layer number
            bool column1layer5 = GetLayerStatus(7, 5);

            /// Returns layer status by the index of column and layer number
            bool column2layer2 = GetLayerStatus(8, 2);

            /// Returns layer status by the index of column and layer number
            bool column2layer3 = GetLayerStatus(8, 3);

            /// Returns layer status by the index of column and layer number
            bool column2layer4 = GetLayerStatus(8, 4);

            /// Returns layer status by the index of column and layer number
            bool column2layer5 = GetLayerStatus(8, 5);

            /// Returns layer status by the index of column and layer number
            bool column3layer2 = GetLayerStatus(9, 2);

            /// Returns layer status by the index of column and layer number
            bool column3layer3 = GetLayerStatus(9, 3);

            /// Returns layer status by the index of column and layer number
            bool column3layer4 = GetLayerStatus(9, 4);

            /// Returns layer status by the index of column and layer number
            bool column3layer5 = GetLayerStatus(9, 5);

            /// Returns layer status by the index of column and layer number
            bool column4layer2 = GetLayerStatus(10, 2);

            /// Returns layer status by the index of column and layer number
            bool column4layer3 = GetLayerStatus(10, 3);

            /// Returns layer status by the index of column and layer number
            bool column4layer4 = GetLayerStatus(10, 4);

            /// Returns layer status by the index of column and layer number
            bool column4layer5 = GetLayerStatus(10, 5);

            /// Returns layer status by the index of column and layer number
            bool column5layer2 = GetLayerStatus(11, 2);

            /// Returns layer status by the index of column and layer number
            bool column5layer3 = GetLayerStatus(11, 3);

            /// Returns layer status by the index of column and layer number
            bool column5layer4 = GetLayerStatus(11, 4);

            /// Returns layer status by the index of column and layer number
            bool column5layer5 = GetLayerStatus(11, 5);

            /// Returns layer status by the index of column and layer number
            bool column6layer2 = GetLayerStatus(12, 2);

            /// Returns layer status by the index of column and layer number
            bool column6layer3 = GetLayerStatus(12, 3);

            /// Returns layer status by the index of column and layer number
            bool column6layer4 = GetLayerStatus(12, 4);

            /// Returns layer status by the index of column and layer number
            bool column6layer5 = GetLayerStatus(12, 5);

            /// Returns layer status by the index of column and layer number
            bool column7layer2 = GetLayerStatus(13, 2);

            /// Returns layer status by the index of column and layer number
            bool column7layer3 = GetLayerStatus(13, 3);

            /// Returns layer status by the index of column and layer number
            bool column7layer4 = GetLayerStatus(13, 4);

            /// Returns layer status by the index of column and layer number
            bool column7layer5 = GetLayerStatus(13, 5);

            /// Returns layer status by the index of column and layer number
            bool column8layer2 = GetLayerStatus(14, 2);

            /// Returns layer status by the index of column and layer number
            bool column8layer3 = GetLayerStatus(14, 3);

            /// Returns layer status by the index of column and layer number
            bool column8layer4 = GetLayerStatus(14, 4);

            /// Returns layer status by the index of column and layer number
            bool column8layer5 = GetLayerStatus(14, 5);

            /// Checks if layer 5 is active in the closest columns
            if (column1layer5 || column2layer5 || column3layer5 || column4layer5 || column5layer5 || column6layer5 || column7layer5 || column8layer5)
            {
                /// Sets click allowed status of the layer 1 to false
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer3.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer4.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 4 is active in the closest columns
            if (column1layer4 || column2layer4 || column3layer4 || column4layer4 || column5layer4 || column6layer4 || column7layer4 || column8layer4)
            {
                /// Sets click allowed status of the layer 1 to false
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer3.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 3 is active in the closest columns
            if (column1layer3 || column2layer3 || column3layer3 || column4layer3 || column5layer3 || column6layer3 || column7layer3 || column8layer3)
            {
                /// Sets click allowed status of the layer 1 to false
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);

                /// Sets click allowed status of the layer 1 to false
                layer2.GetComponent<TileLayer>().SetClickAllowed(false);
            }

            /// Checks if layer 2 is active in the closest columns
            if (column1layer2 || column2layer2 || column3layer2 || column4layer2 || column5layer2 || column6layer2 || column7layer2 || column8layer2)
            {
                /// Sets click allowed status of the layer 1 to false
                layer1.GetComponent<TileLayer>().SetClickAllowed(false);
            }
        }

        /// <summary>
        /// Returns layer status by the index of column and layer number
        /// </summary>
        private bool GetLayerStatus(int indexOfColumn, int layerIndex)
        {
            /// Corrents index to use in the list
            indexOfColumn--;

            /// Checks if close column exists
            if (closeTileColumns[indexOfColumn] != null)
            {
                /// Check if layerIndex is equal to 1
                if (layerIndex == 1)
                {
                    return closeTileColumns[indexOfColumn].layer1Active;
                }

                /// Check if layerIndex is equal to 2
                else if (layerIndex == 2)
                {
                    return closeTileColumns[indexOfColumn].layer2Active;
                }

                /// Check if layerIndex is equal to 3
                else if (layerIndex == 3)
                {
                    return closeTileColumns[indexOfColumn].layer3Active;
                }

                /// Check if layerIndex is equal to 4
                else if (layerIndex == 4)
                {
                    return closeTileColumns[indexOfColumn].layer4Active;
                }

                /// Check if layerIndex is equal to 5
                else if (layerIndex == 5)
                {
                    return closeTileColumns[indexOfColumn].layer5Active;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the colunm layers active status 
        /// </summary>
        internal void SetLayersActiveStatus(string layer1Status, string layer2Status, string layer3Status, string layer4Status, string layer5Status)
        {
            /// Check if layer 1 status is occupied
            if (layer1Status == "occupied")
            {
                /// Sets the layer 1 active status to true
                layer1Active = true;
            }
            else
            {
                /// Deactivates gameObject of layer 1
                layer1.SetActive(false);
            }

            /// Check if layer 1 status is occupied
            if (layer2Status == "occupied")
            {
                /// Sets the layer 2 active status to true
                layer2Active = true;
            }
            else
            {
                /// Deactivates gameObject of layer 2
                layer2.SetActive(false);
            }

            /// Check if layer 1 status is occupied
            if (layer3Status == "occupied")
            {
                /// Sets the layer 3 active status to true
                layer3Active = true;
            }
            else
            {
                /// Deactivates gameObject of layer 3
                layer3.SetActive(false);
            }

            /// Check if layer 1 status is occupied
            if (layer4Status == "occupied")
            {
                /// Sets the layer 4 active status to true
                layer4Active = true;
            }
            else
            {
                /// Deactivates gameObject of layer 4
                layer4.SetActive(false);
            }

            /// Check if layer 1 status is occupied
            if (layer5Status == "occupied")
            {
                /// Sets the layer 5 active status to true
                layer5Active = true;
            }
            else
            {
                /// Deactivates gameObject of layer5
                layer5.SetActive(false);
            }
        }

        /// <summary>
        /// Returns TileLayer class of the layer 1 gameObject
        /// </summary>
        internal TileLayer GetLayer1()
        {
            return layer1.GetComponent<TileLayer>();
        }

        /// <summary>
        /// Returns TileLayer class of the layer 2 gameObject
        /// </summary>
        internal TileLayer GetLayer2()
        {
            return layer2.GetComponent<TileLayer>();
        }

        /// <summary>
        /// Returns TileLayer class of the layer 3 gameObject
        /// </summary>
        internal TileLayer GetLayer3()
        {
            return layer3.GetComponent<TileLayer>();
        }

        /// <summary>
        /// Returns TileLayer class of the layer 4 gameObject
        /// </summary>
        internal TileLayer GetLayer4()
        {
            return layer4.GetComponent<TileLayer>();
        }

        /// <summary>
        /// Returns TileLayer class of the layer 5 gameObject
        /// </summary>
        internal TileLayer GetLayer5()
        {
            return layer5.GetComponent<TileLayer>();
        }

        /// <summary>
        /// Returns first upper layer in the column as TileLayer class 
        /// </summary>
        internal TileLayer GetUpperLayer()
        {
            /// Checks if layer 5 active status is true and the layer 5 is not used
            if (layer5Active && !layer5Used)
            {
                /// Set used status of the layer 5
                layer5Used = true;

                return layer5.GetComponent<TileLayer>();
            }

            /// Checks if layer 4 active status is true and the layer 4 is not used
            else if (layer4Active && !layer4Used)
            {
                /// Set used status of the layer 5
                layer4Used = true;
                return layer4.GetComponent<TileLayer>();
            }

            /// Checks if layer 3 active status is true and the layer 3 is not used
            else if (layer3Active && !layer3Used)
            {
                /// Set used status of the layer 5
                layer3Used = true;

                ///
                return layer3.GetComponent<TileLayer>();
            }

            /// Checks if layer 2 active status is true and the layer 2 is not used
            else if (layer2Active && !layer2Used)
            {
                /// Set used status of the layer 5
                layer2Used = true;

                ///
                return layer2.GetComponent<TileLayer>();
            }

            /// Checks if layer 1 active status is true and the layer 2 is not used
            else if (layer1Active && !layer1Used)
            {
                /// Set used status of the layer 5
                layer1Used = true;

                ///
                return layer1.GetComponent<TileLayer>();
            }

            return null;
        }
    }
}
