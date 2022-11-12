// Author: Oleksii Stepanov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MahjongTemplateEditor 
{
    /// <summary>
    /// Class that manages templates of the column list 
    /// </summary>
    internal class TemplateManager : MonoBehaviour
    {
        internal static TemplateManager Instance;

        /// <summary>
        /// Animator of the object
        /// </summary>
        private Animator anim;

        /// <summary>
        /// Last tile id
        /// </summary>
        private string lastTilesId = "";

        /// <summary>
        /// Amount of combination of the template
        /// </summary>
        private int maxCombinations = 0;

        /// <summary>
        /// Amount of matches
        /// </summary>
        private int matchCount = 0;

        /// <summary>
        /// Knock layer order
        /// </summary>
        private int knockTilesSortingLayer = 0;

        /// <summary>
        /// GameObject of the tile that was selected
        /// </summary>
        private GameObject layer1selected;

        /// <summary>
        /// GameObject of the tile number 2 that was selected
        /// </summary>
        private GameObject layer2selected;

        /// <summary>
        /// Current template
        /// </summary>
        internal Template currentTemplate;

        /// <summary>
        /// List of the columns
        /// </summary>
        [SerializeField] private List<TileColumn> tileColumns = new List<TileColumn>();

        /// <summary>
        /// List of the columns that template is using
        /// </summary>
        [SerializeField] private List<TileColumn> tileColumnsInUse = new List<TileColumn>();

        /// <summary>
        /// List of the tiles 
        /// </summary>
        [SerializeField] private List<TileLayer> tiles = new List<TileLayer>();

        /// <summary>
        /// Sequence of the template
        /// </summary>
        [SerializeField] private List<string> sequence = new List<string>();

        /// <summary>
        /// Two center tiles
        /// </summary>
        [SerializeField] private List<TileLayer> twoCenterTiles = new List<TileLayer>();

        /// <summary>
        /// Two center columns
        /// </summary>
        [SerializeField] private List<TileColumn> twoCenterColumns = new List<TileColumn>();


        /// <summary>
        ///  Creation of the singleton
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            /// Assigns Animator component of the object to variable
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Sets up manager - loads template by number
        /// </summary>
        internal void SetUp(int value)
        {
            /// Sets the amount of matches to 0
            matchCount = 0;
            
            /// Resets all tiles 
            ResetAllTiles();

            /// Loads template by its number
            XMLTemplateManager.Instance.LoadTemplate(value.ToString());

            // Show template
            Show(true);
        }

        /// <summary>
        /// Shows template
        /// </summary>
        internal void Show(bool value)
        {
            ///
            anim.SetBool("Show", value);
        }

        /// <summary>
        /// Sets template by Template class
        /// </summary>
        internal void SetTemplate(Template value)
        { 
            /// Sets current template by value
            currentTemplate = value;
            
            /// Creates a field of the template
            CreateField();
            
            /// Sets the ids for all tiles that are in use 
            SetIDs();
        }

        /// <summary>
        /// Check click allowed status of the tiles in the columns
        /// </summary>
        internal void CheckClickAllow()
        {
            /// Runs list for all columns in use
            for (int i = 0; i < tileColumnsInUse.Count; i++)
            {
                /// Check click allowed status for the layers in the column
                tileColumnsInUse[i].SetClickAllow();
            }
        }

        /// <summary>
        /// Returns column by its id
        /// </summary>
        internal TileColumn GetTileColumn(string id)
        {
            /// Runs loop
            for (int i = 0; i < tileColumns.Count; i++)
            {
                /// Check is column id is equal to right id
                if (tileColumns[i].id == id)
                {
                    return tileColumns[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Assigns to two tiles and than check if ids of the two selected tiles are equal 
        /// </summary>
        internal void SetTilesToCheck(GameObject value)
        {
            /// Check if first tile to check is null
            if (layer1selected == null)
            {
                /// Assigns first tile 
                layer1selected = value;
                
                /// Sets the position for TilePointer
                TilePointer.Instance.SetTargetPos(layer1selected.transform);
            }
            else
            {
                /// Assigns second tile 
                layer2selected = value;

                /// Check if two selected tiles are equal
                Check();
            }
        }

        /// <summary>
        /// Resets selected tile 1 
        /// </summary>
        internal void ResetLayer1()
        {
            ///
            layer1selected = null;
        }

        /// <summary>
        /// Check if two selected tiles are equal and performs knock
        /// </summary>
        private void Check()
        {
            /// Assigns id of the tile 1
            string id1 = layer1selected.GetComponent<TileLayer>().GetID();

            /// Assigns id of the tile 2
            string id2 = layer2selected.GetComponent<TileLayer>().GetID();

            /// Check if two ids are equal
            if (id1 == id2)
            {
                /// Hide tile 1
                layer1selected.GetComponent<TileLayer>().Hide();

                /// Hide tile 2
                layer2selected.GetComponent<TileLayer>().Hide();

                /// Creates and Assigns Knock Manager script
                KnockManager knockManager = gameObject.AddComponent<KnockManager>();

                /// Sets up Knock Manager 
                knockManager.SetUp(layer1selected, layer2selected, knockTilesSortingLayer);

                /// Update the knock sorting layer order nubmer 
                knockTilesSortingLayer = knockTilesSortingLayer + 2;

                /// Check click allow status after knock
                CheckClickAllow();

                /// Removes tile from the list
                RemoveFromList(layer1selected.GetComponent<TileLayer>().layerMatrixName);

                /// Removes tile from the list
                RemoveFromList(layer2selected.GetComponent<TileLayer>().layerMatrixName);

                /// Reset tile 1 gameobject
                layer1selected = null;

                /// Reset tile 2 gameobject
                layer2selected = null;

                /// Reset pointer 
                TilePointer.Instance.ResetPointer();

                /// Update the amount of matches
                matchCount++;

                /// Checks if game is over 
                CheckGameOver();
            }
            else
            {
                /// Resets the tile 1
                layer1selected.GetComponent<TileLayer>().ResetTile();

                /// Assigns layer 1 gameobject with layer 2 gameobject 
                layer1selected = layer2selected;

                /// Update the TilePointer position
                TilePointer.Instance.SetTargetPos(layer1selected.transform);
            }
        }

        /// <summary>
        /// Removes tile from the list of used tiles
        /// </summary>
        private void RemoveFromList(string tilePlace)
        {
            /// Index of the tile 
            int indexToRemove = 999;

            /// Runs loop 
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Check if matrix name if equal to tile place
                if (tiles[i].layerMatrixName == tilePlace)
                {
                    /// Assigns index to remove 
                    indexToRemove = i;
                }
            }

            /// Check if index to remove is changed
            if (indexToRemove != 999)
            {
                /// Removes tile for the list of tiles by index
                tiles.RemoveAt(indexToRemove);
            }
        }

        /// <summary>
        /// Check if game is over and there is no combinations
        /// </summary>
        internal void CheckGameOver()
        {
            /// Checks if amount of matches is equal to max of tiles
            if (matchCount == maxCombinations)
            {
                /// Start Coroutine
                StartCoroutine(WaitThenSetUpWin());
            }
            else
            {
                /// Checks if condination exists and assigns result to variable
                bool combinationExists = CheckCombination();

                /// Check if combination is not exists 
                if (!combinationExists)
                {
                    /// Sets up the game over 
                    SetUpGameOver();
                }
            }
        }

        /// <summary>
        /// Waits 0.5 seconds than hide template than again waits 0.5 seconds and resets all the tiles
        /// </summary>
        private IEnumerator WaitThenSetUpWin()
        {
            /// Waits 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            /// Sets the match count to zero
            matchCount = 0;

            /// Hides template
            Show(false);

            /// Waits 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            /// Resets all tiles
            ResetAllTiles();
        }

        /// <summary>
        /// Sets up game over
        /// </summary>
        private void SetUpGameOver()
        {
            //// Shuffles all tiles
            Shuffle();
        }

        /// <summary>
        /// Checks if combination exists on the field 
        /// </summary>
        internal bool CheckCombination()
        {
            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Runs loop
                for (int j = 0; j < tiles.Count; j++)
                {
                    /// Check if click is allowed
                    if (tiles[i].GetClickAllowed())
                    {
                        /// Check if click is allowed
                        if (tiles[j].GetClickAllowed())
                        {
                            /// Check if tile is not hidden
                            if (!tiles[j].hidden)
                            {
                                /// Check if tile is not hidden
                                if (!tiles[i].hidden)
                                {
                                    /// Check if tile is not equel to another tile
                                    if (tiles[i] != tiles[j])
                                    {
                                        /// Check if tile id is equel to another tile
                                        if (tiles[i].GetID() == tiles[j].GetID())
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        ////////////////////////////////////////////////////////////////////////// Creation \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Resets all tiles
        /// </summary>
        internal void ResetAllTiles()
        {
            /// Clears column in use 
            tileColumnsInUse.Clear();

            /// Clears sequence
            sequence.Clear();

            /// Clears tiles
            tiles.Clear();

            /// Runs loop
            for (int i = 0; i < tileColumns.Count; i++)
            {
                /// Resets all columns 
                tileColumns[i].ResetAll();
            }
        }

        /// <summary>
        /// Creats field 
        /// </summary>
        internal void CreateField()
        {
            /// Runs loop
            for (int i = 0; i < tileColumns.Count; i++)
            {
                /// Runs loop
                for (int j = 0; j < currentTemplate.tiles.Count; j++)
                {
                    /// Checks if column tile id equels to current template tile id 
                    if (tileColumns[i].id == currentTemplate.tiles[j].id)
                    {
                        /// Sets active status to the column layers
                        tileColumns[i].SetLayersActiveStatus(currentTemplate.tiles[j].layer1status, currentTemplate.tiles[j].layer2status, currentTemplate.tiles[j].layer3status, currentTemplate.tiles[j].layer4status, currentTemplate.tiles[j].layer5status);

                        /// Checks if column layer 1 is occupied 
                        if (currentTemplate.tiles[j].layer1status == "occupied")
                        {
                            /// Adds layer 1 to the list of tiles
                            tiles.Add(tileColumns[i].GetLayer1());

                            /// Shows tile gameObject
                            tileColumns[i].GetLayer1().gameObject.SetActive(true);
                        }

                        /// Checks if column layer 2 is occupied
                        if (currentTemplate.tiles[j].layer2status == "occupied")
                        {
                            /// Add layer 2 to the list of tiles
                            tiles.Add(tileColumns[i].GetLayer2());

                            /// Shows tile gameObject
                            tileColumns[i].GetLayer2().gameObject.SetActive(true);
                        }

                        /// Checks if column layer 3 is occupied
                        if (currentTemplate.tiles[j].layer3status == "occupied")
                        {
                            /// Add layer 3 to the list of tiles
                            tiles.Add(tileColumns[i].GetLayer3());

                            /// Shows tile gameObject
                            tileColumns[i].GetLayer3().gameObject.SetActive(true);
                        }

                        /// Checks if column layer 4 is occupied
                        if (currentTemplate.tiles[j].layer4status == "occupied")
                        {
                            /// Add layer 4 to the list of tiles
                            tiles.Add(tileColumns[i].GetLayer4());

                            /// Shows tile gameObject
                            tileColumns[i].GetLayer4().gameObject.SetActive(true);
                        }

                        /// Checks if column layer 5 is occupied
                        if (currentTemplate.tiles[j].layer5status == "occupied")
                        {
                            /// Add layer 5 to the list of tiles
                            tiles.Add(tileColumns[i].GetLayer5());

                            /// Shows tile gameObject
                            tileColumns[i].GetLayer5().gameObject.SetActive(true);
                        }

                        /// Checks if any of the columns layers are occupied 
                        if (currentTemplate.tiles[j].layer1status == "occupied" || currentTemplate.tiles[j].layer2status == "occupied" || currentTemplate.tiles[j].layer3status == "occupied" || currentTemplate.tiles[j].layer4status == "occupied" || currentTemplate.tiles[j].layer5status == "occupied")
                        {
                            /// Adds column to the list 
                            tileColumnsInUse.Add(tileColumns[i]);
                        }
                    }
                }
            }

            /// Assigns the number of possible combinations
            maxCombinations = tiles.Count / 2;
        }

        /// <summary>
        /// Sets the ids for the field
        /// </summary>
        private void SetIDs()
        {
            /// Check click allow status of all tiles
            CheckClickAllow();

            /// Reset tile pointer position
            TilePointer.Instance.ResetPointer();

            /// Creates list of the ids
            List<string> tileIDs = CreateIdList();

            /// Clears sequence
            sequence.Clear();

            /// Runs loop
            for (int i = 0; i < tiles.Count / 2; i++)
            {
                /// Returns two removed tile ids
                string strings = GetRemovedTwoTiles();

                /// Checks if value of strings is not empty
                if (strings != "")
                {
                    /// Creates array of the ids
                    string[] splitArray = strings.Split(char.Parse("/"));

                    /// Assigns id of the tile 1
                    string id1 = splitArray[0];

                    /// Assigns id of the tile 2
                    string id2 = splitArray[1];

                    /// Adds tile 1 id to the list
                    sequence.Add(id1);

                    /// Adds tile 2 id to the list
                    sequence.Add(id2);

                    /// Check click allowed
                    CheckClickAllow();
                }
            }

            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Reset two tiles
                tiles[i].ResetForTwo();
            }

            /// Counter of ids
            int counterOfID = 0;

            /// Id to set 
            string idToSet = "";

            /// Runs loop 
            for (int i = 0; i < sequence.Count; i++)
            {
                /// Checks if counter of id equals zero
                if (counterOfID == 0)
                {
                    /// Checks if amount of the tile Ids equals zero
                    if (tileIDs.Count == 0)
                    {
                        /// Creates list of ids
                        tileIDs = CreateIdList();
                    }

                    /// Generate random number 
                    int idNumber = Random.Range(0, tileIDs.Count - 1);

                    /// Sets the id from list of ids
                    idToSet = tileIDs[idNumber];

                    /// Increase counter of id by 2
                    counterOfID = 2;

                    /// Remove number of ids list
                    tileIDs.RemoveAt(idNumber);
                }

                /// Sets the id to the layer
                GetLayerById(sequence[i]).SetID(idToSet);

                /// Decrease counter of id by 1
                counterOfID--;
            }

            /// List of the empty tiles
            List<string> emptyList = GetEmptyTiles();

            /// Counter of id is set to zero
            counterOfID = 0;

            /// Runs loop
            if (emptyList.Count > 0)
            {
                /// Runs loop
                for (int i = 0; i < emptyList.Count; i++)
                { 
                    /// Check if counter of id equals zero
                    if (counterOfID == 0)
                    {
                        /// Check if number of id equals zero
                        if (tileIDs.Count == 0)
                        {
                            /// Create a new id list
                            tileIDs = CreateIdList();
                        }

                        /// Generate random id number
                        int idNumber = Random.Range(0, tileIDs.Count - 1);

                        /// Set the counter of id to 2 
                        counterOfID = 2;

                        /// Remove number from  tile id list
                        tileIDs.RemoveAt(idNumber);
                    }

                    /// Sets the id to the layer
                    GetLayerById(emptyList[i]).SetID(idToSet);
                    
                    /// Decrease counter of id by 1
                    counterOfID--;
                }
            }

            /// Clear id list
            tileIDs.Clear();

            /// Wait than check click allow
            StartCoroutine(WaitThenCheckAllow());
        }

        /// <summary>
        /// Waits 0.1 second and check click allow
        /// </summary>
        private IEnumerator WaitThenCheckAllow()
        {
            ///
            yield return new WaitForSeconds(0.1f);

            ///
            CheckClickAllow();
        }

        /// <summary>
        /// Returns list of tiles without id
        /// </summary>
        private List<string> GetEmptyTiles()
        {
            /// Result list
            List<string> resultList = new List<string>();

            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Checks if id of the tile is empty
                if (tiles[i].GetID() == "")
                {
                    /// Add tile to the list
                    resultList.Add(tiles[i].layerMatrixName);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Creates list of ids
        /// </summary>
        private List<string> CreateIdList()
        {
            /// Result list 
            List<string> resultList = new List<string>();

            /// Runs loop
            for (int i = 1; i < 44; i++)
            {
                ///Adds id to result list
                resultList.Add(i.ToString());
            }

            /// Runs loop
            for (int i = 1; i < 19; i++)
            {
                ///Adds id to result list
                resultList.Add(i.ToString());
            }

            /// Runs loop
            for (int i = 27; i < 44; i++)
            {
                ///Adds id to result list
                resultList.Add(i.ToString());
            }

            return resultList;
        }

        /// <summary>
        /// Returns Tile Layer object by id
        /// </summary>
        private TileLayer GetLayerById(string sequenceId)
        {
            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Checks if layer matrix name equals to sequence id
                if (tiles[i].layerMatrixName == sequenceId)
                {
                    return tiles[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns two ids of the two removed tiles
        /// </summary>
        internal string GetRemovedTwoTiles()
        {
            /// Result variable
            string result = "";

            /// Gets list of click allow layers
            var allowList = GetClickAllowTiles();

            /// Check if the list is bigger the 1
            if (allowList.Count > 1)
            {
                /// Random tile index
                int randomTile = Random.Range(0, allowList.Count);

                /// Adds to result variable layer matrix name of random tile
                result = allowList[randomTile].layerMatrixName;

                /// Remove random tile from the sequence 
                allowList[randomTile].RemoveForSequence();

                /// Remove random tile from the click allow list 
                allowList.RemoveAt(randomTile);

                /// Random tile index
                randomTile = Random.Range(0, allowList.Count);

                /// Adds to result variable layer matrix name of random tile
                result = result + "/" + allowList[randomTile].layerMatrixName;

                /// Remove random tile from the sequence 
                allowList[randomTile].RemoveForSequence();

                /// Remove random tile from the click allow list 
                allowList.RemoveAt(randomTile);

                /// Clear allow list
                allowList.Clear();
            }

            return result;
        }

        /// <summary>
        /// Returns list of the click allowed tiles
        /// </summary>
        private List<TileLayer> GetClickAllowTiles()
        {
            /// New list of tiles
            List<TileLayer> resultList = new List<TileLayer>();

            /// Runs loop 
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Checks if tiles is click allowed
                if (tiles[i].GetClickAllowed())
                {
                    /// Checks if tiles is not hidden
                    if (!tiles[i].hidden)
                    {
                        /// Adds to the list
                        resultList.Add(tiles[i]);
                    }
                }
            }

            return resultList;
        }

        ////////////////////////////////////////////////////////////////// Tools \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Removes two tiles
        /// </summary>
        internal void RemoveTwoTiles()
        {
            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Runs loop
                for (int j = 0; j < tiles.Count; j++)
                {
                    /// Check click alloed status of the tile
                    if (tiles[i].GetClickAllowed())
                    {
                        /// Check click alloed status of the tile
                        if (tiles[j].GetClickAllowed())
                        {
                            /// Checks if tiles is not hidden
                            if (!tiles[j].hidden)
                            {
                                /// Checks if tiles is not hidden
                                if (!tiles[i].hidden)
                                {
                                    /// Checks if tiles are not the same
                                    if (tiles[i] != tiles[j])
                                    {
                                        /// Checks if tiles id are the same
                                        if (tiles[i].GetID() == tiles[j].GetID())
                                        {
                                            /// Select tile 1 
                                            tiles[i].SelectTile();

                                            /// Select tile 1 
                                            tiles[j].SelectTile();

                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Highlight possible combination
        /// </summary>
        internal void HightlightTwoTiles()
        {
            /// Runs loop
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Runs loop
                for (int j = 0; j < tiles.Count; j++)
                {
                    /// Check click alloed status of the tile
                    if (tiles[i].GetClickAllowed())
                    {
                        /// Check click alloed status of the tile
                        if (tiles[j].GetClickAllowed())
                        {
                            /// Checks if tiles is not hidden
                            if (!tiles[j].hidden)
                            {
                                /// Checks if tiles is not hidden
                                if (!tiles[i].hidden)
                                {
                                    /// Checks if tiles are not the same
                                    if (tiles[i] != tiles[j])
                                    {
                                        /// Checks if tiles id are the same
                                        if (tiles[i].GetID() == tiles[j].GetID())
                                        {
                                            /// Highlight the tile 1
                                            tiles[i].HighlightTile();

                                            /// Highlight the tile 2
                                            tiles[j].HighlightTile();
                                            
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Shuffles the field
        /// </summary>
        internal void Shuffle()
        {
            /// Reset tile pointer position
            TilePointer.Instance.ResetPointer();

            /// Checks if number of tiles equals to 2
            if (tiles.Count == 2)
            {
                /// Show two center tiles
                ShowTwoCenterTiles();

                /// Runs loop
                for (int i = 0; i < tiles.Count; i++)
                {
                    /// Check if tiles are active in hierarchy
                    if (tiles[i].gameObject.activeInHierarchy)
                    {
                        /// Sets the id for the tile
                        tiles[i].GetComponent<TileLayer>().SetID(lastTilesId);
                    }
                }
            }
            else
            {
                /// Sets the ids for the tiles 
                SetIDs();
            }
        }

        /// <summary>
        /// Shows two center tiles
        /// </summary>
        internal void ShowTwoCenterTiles()
        {
            /// Set last tile id
            lastTilesId = GetLastId();

            /// Runs loop 
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Hides tile
                tiles[i].gameObject.SetActive(false);
            }

            /// Clears tiles list
            tiles.Clear();

            /// Clears list of column 
            tileColumnsInUse.Clear();

            /// Adds tile to the list of the tiles
            tiles.Add(twoCenterTiles[0]);

            /// Adds tile to the list of the tiles
            tiles.Add(twoCenterTiles[1]);

            /// Adds column to the list 
            tileColumnsInUse.Add(tiles[0].transform.parent.GetComponent<TileColumn>());

            /// Adds column to the list 
            tileColumnsInUse.Add(tiles[1].transform.parent.GetComponent<TileColumn>());

            /// Sets layer 2 active status
            tileColumnsInUse[0].layer2Active = false;

            /// Sets layer 2 active status
            tileColumnsInUse[1].layer2Active = false;

            /// Sets layer 3 active status
            tileColumnsInUse[0].layer3Active = false;

            /// Sets layer 3 active status
            tileColumnsInUse[1].layer3Active = false;

            /// Sets layer 4 active status
            tileColumnsInUse[0].layer4Active = false;

            /// Sets layer 4 active status
            tileColumnsInUse[1].layer4Active = false;

            /// Sets layer 5 active status
            tileColumnsInUse[0].layer5Active = false;

            /// Sets layer 5 active status
            tileColumnsInUse[1].layer5Active = false;

            /// Resets tile after creation
            tiles[0].ResetAfterCreation();

            /// Resets tile after creation
            tiles[1].ResetAfterCreation();

            /// Runs loop 
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Destroys TileLayerHidder class from tile object
                Destroy(tiles[i].GetComponent<TileLayerHidder>());

                /// Sets the parent of the tile
                tiles[i].transform.parent = twoCenterColumns[i].transform;

                /// Show tile 
                tiles[i].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Returns id of the last tile
        /// </summary>
        private string GetLastId()
        {
            /// Runs loop 
            for (int i = 0; i < tiles.Count; i++)
            {
                /// Checks if tile is active in Hierarchy
                if (tiles[i].gameObject.activeInHierarchy)
                {
                    return tiles[i].GetComponent<TileLayer>().GetID();
                }
            }

            return "";
        }
    }
}
