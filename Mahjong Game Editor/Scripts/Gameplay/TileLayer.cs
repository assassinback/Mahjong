// Author: Oleksii Stepanov

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class that manages layer object of the tile column 
    /// </summary>
    internal class TileLayer : MonoBehaviour
    {
        /// <summary>
        /// Id of the layer
        /// </summary>
        private string id = "";

        /// <summary>
        /// Matrix name of the object
        /// </summary>
        internal string layerMatrixName = "";

        /// <summary>
        /// Click allowed false 
        /// </summary>
        private bool clickAllowed = true;

        /// <summary>
        /// Sprite Renderer of the object
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Active status of the layer
        /// </summary>
        internal bool active = false;

        /// <summary>
        /// Hidden status of the layer
        /// </summary>
        internal bool hidden = false;

        /// <summary>
        /// Index of position in column 
        /// </summary>
        private int upperIndex = 0;

        private void Awake()
        {
            /// Assigns SpriteRenderer component to the variable
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            /// Sets index of the position in column
            SetUpperIndex();
        }

        /// <summary>
        /// Sets index of the position in column
        /// </summary>
        internal void SetUpperIndex()
        {
            /// Checks if gameObject name is equal to TileLayer5
            if (gameObject.name == "TileLayer5")
            {
                /// Sets upperIndex to 5
                upperIndex = 5;
            }
            /// Checks if gameObject name is equal to TileLayer4
            else if (gameObject.name == "TileLayer4")
            {
                /// Sets upperIndex to 4
                upperIndex = 4;
            }
            /// Checks if gameObject name is equal to TileLayer3
            else if (gameObject.name == "TileLayer3")
            {
                /// Sets upperIndex to 3
                upperIndex = 3;
            }
            /// Checks if gameObject name is equal to TileLayer2
            else if (gameObject.name == "TileLayer2")
            {
                /// Sets upperIndex to 2
                upperIndex = 2;
            }
            /// Checks if gameObject name is equal to TileLayer1
            else if (gameObject.name == "TileLayer1")
            {
                /// Sets upperIndex to 1
                upperIndex = 1;
            }
        }

        /// <summary>
        /// Return index of position in column
        /// </summary>
        internal int GetUpperIndex()
        {
            ///
            return upperIndex;
        }

        /// <summary>
        /// Sets id for layer and updates its sprite
        /// </summary>
        internal void SetID(string value)
        {
            /// Sets id
            id = value;

            /// Updates sprite of the layer
            UpdateSprite();
        }

        /// <summary>
        /// Returns id of the layer
        /// </summary>
        internal string GetID()
        {
            return id;
        }

        /// <summary>
        /// Updates sprite of the layer
        /// </summary>
        private void UpdateSprite()
        {
            /// Start of coroutine
            StartCoroutine(SetImage());
        }

        /// <summary>
        /// Finds image in the folder by path and sets sprite of the layer
        /// </summary>
        private IEnumerator SetImage()
        {
            /// Id Variable
            string nameOfSprite = id;
            
            /// Number of the sprite
            int numberOfSprite = int.Parse(id);

            /// Checks if number of sprite is lower that 10
            if (numberOfSprite < 10)
            {
                /// Sets the name of the sprite
                nameOfSprite = "Tile0"+id;
            }
            else 
            {
                /// Sets the name of the sprite
                nameOfSprite = "Tile"+id;
            }

            /// Finds required image in the folder by path
            using (UnityWebRequest loader = UnityWebRequestTexture.GetTexture(GetPath() + nameOfSprite + ".png"))
            {
                /// Enables UnityWebRequest
                yield return loader.SendWebRequest();

                /// Check is path was correct and image is loaded
                if (string.IsNullOrEmpty(loader.error))
                {
                    /// Assigns image to the layers SpriteRenderer
                    spriteRenderer.sprite = SpriteFromTexture2D(DownloadHandlerTexture.GetContent(loader));
                }
                else
                {
                    /// Print error message
                    print("Error: Check path and file name");
                }
            }
        }

        /// <summary>
        /// Return path to the folder of tile images 
        /// </summary>
        private string GetPath()
        {
            /// Result variable
            string result = "";

            /// Checks if project is running of OSX platform
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                /// Assings path to the result varialbe
                result = "file://" + Application.dataPath + "/Mahjong Game Editor/Art/Tiles/";
            }
            else
            {
                /// Assings path to the result varialbe
                result = Application.dataPath + "/Mahjong Game Editor/Art/Tiles/";
            }

            return result;
        }

        /// <summary>
        /// Returns sprite from converted texture2d
        /// </summary>
        private Sprite SpriteFromTexture2D(Texture2D texture)
        {
            /// Sprite creation
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        /// <summary>
        /// Returns click allowed flag
        /// </summary>
        internal bool GetClickAllowed()
        {
            return clickAllowed;
        }

        private void OnMouseDown()
        {
            /// Check if click is allowed
            if (clickAllowed)
            {
                /// Checks if layer is active
                if (!active)
                {
                    /// Selects the layer
                    SelectTile();
                }
                else
                {
                    /// Resets first layer that was selected
                    TemplateManager.Instance.ResetLayer1();
                    //TemplateManager.Instance.ResetLayer2();
                    //TemplateManager.Instance.ResetLayer3();
                    //TemplateManager.Instance.ResetLayer1();

                    /// Resets this layer
                    ResetTile();
                }
            }

        }

        /// <summary>
        /// Selects layer 
        /// </summary>
        internal void SelectTile()
        {
            /// Sets the active flag to true
            active = true;
            
            /// Set the one of two layes that will be check
            TemplateManager.Instance.SetTilesToCheck(gameObject);
        }

        /// <summary>
        /// Removes layer id from sequence 
        /// </summary>
        internal void RemoveForSequence()
        {
            /// Sets the hidden flag to true
            hidden = true;

            /// Diactivates layer in its column
            transform.parent.GetComponent<TileColumn>().DeactivateLayer(gameObject.name);

            /// Changes color of the layer
            spriteRenderer.color = Color.white;

            /// Diactivates gameObject
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Resets information after creation
        /// </summary>
        internal void ResetAfterCreation()
        {
            /// Sets id to ""
            id = "";

            /// Sets active flag to false
            active = false;

            /// Sets hidder flag to false
            hidden = false;

            /// Sets the color of sprite renderer 
            spriteRenderer.color = new Color(255, 255, 255, 1);

            /// Sets click allowed falg to true
            SetClickAllowed(true);

            /// Activates gameObject
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void ResetForTwo()
        {
            /// Sets hidder flag to false
            hidden = false;

            /// Sets the color of sprite renderer 
            spriteRenderer.color = new Color(255, 255, 255, 1);

            /// Activates layer in its column
            transform.parent.GetComponent<TileColumn>().ActivateLayer(gameObject.name);

            /// Activates gameObject
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Highlights tile 
        /// </summary>
        internal void HighlightTile()
        {
            /// Changes color of the layer
            spriteRenderer.color = ColorManager.Instance.lightBlue;
        }

        /// <summary>
        /// Hides layer
        /// </summary>
        internal void Hide()
        {
            /// Sets hidder flag to true
            hidden = true;

            /// Dectivates layer in its column
            transform.parent.GetComponent<TileColumn>().DeactivateLayer(gameObject.name);

            /// Sets the color of layer to white
            spriteRenderer.color = Color.white;
        }

        /// <summary>
        /// Diactivates gameObject
        /// </summary>
        internal void DeactivateTile()
        {
            /// Diactivates gameObject
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Resets tile
        /// </summary>
        internal void ResetTile()
        {
            /// Sets the active flag to false
            active = false;

            /// Sets the color of layer to white
            spriteRenderer.color = Color.white;

            /// Sets the pointer position
            TilePointer.Instance.ResetPointer();
        }

        /// <summary>
        /// Sets clicks allowed flag
        /// </summary>
        internal void SetClickAllowed(bool value)
        {
            /// Checks if value is not equal to click allowed status
            if (value != clickAllowed)
            {
                /// Sets click allowed flag to the value 
                clickAllowed = value;

                /// Checks if value is true
                if (value)
                {
                    /// Sets the color of the layer
                    spriteRenderer.color = Color.white;
                }
                else
                {
                    /// Sets the color of the layer
                    spriteRenderer.color = Color.gray;
                }
            }
        }
    }
}
