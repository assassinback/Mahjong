// Author: Oleksii Stepanov

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class that saves/loads templates
    /// </summary>
    internal class SaveTemplateManager : MonoBehaviour
    {
        internal static SaveTemplateManager Instance;

        /// <summary>
        /// Index of current template
        /// </summary>
        private int currentTemplateIndex = 0;

        /// <summary>
        /// Amount of templates
        /// </summary>
        private int templateCount = 0;

        /// <summary>
        /// Current template name
        /// </summary>
        private string currentTemplate = "New Template";

        /// <summary>
        /// Creation of singleton
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
        }

        private void Start()
        {
            /// SetUp Manager
            SetUp();
        }

        /// <summary>
        /// Checks if templates exists
        /// </summary>
        private void SetUp()
        {
            /// Sets amount of templates that are exists
            templateCount = GetNumberOfTemplates();

            /// Checks if amount of templates is bigger that 0
            if (templateCount > 0)
            {
                /// Loads last template
                LoadLastTemplate();
            }
            else
            {
                /// Sets up new template
                SetUpNewTemplate();
            }

            /// Check buttons
            CheckButtons();
        }

        /// <summary>
        /// Sets up manager to load the first template
        /// </summary>
        internal void LoadFirstTemplate()
        {
            /// Check is amount of templates is bigger than 0
            if (templateCount > 0)
            {
                /// Assign current template index to 1
                currentTemplateIndex = 1;
                
                /// Assign name of the template
                currentTemplate = "template1";
                
                /// Loads template
                LoadTemplate(currentTemplateIndex);
            }
        }

        /// <summary>
        /// Sets up manager to load the first template
        /// </summary>
        internal void LoadLastTemplate()
        {
            /// Check is amount of templates is bigger than 0
            if (templateCount > 0)
            {
                /// Assign current template index to total amount of templates
                currentTemplateIndex = templateCount;

                /// Sets name of the template
                currentTemplate = "template" + templateCount.ToString();

                /// Loads template
                LoadTemplate(currentTemplateIndex);
            }
        }

        /// <summary>
        /// Loads next/previous template 
        /// </summary>
        internal void LoadNextTemplate(bool next)
        {
            /// Assign current template index to local variable
            int templateNumber = currentTemplateIndex;

            /// Check if templateNumber should be increased 
            if (next)
            {
                /// Increase templateNumber
                templateNumber++;
            }
            else
            {
                /// Decrease templateNumber
                templateNumber--;
            }

            /// Check is templateNumber is equel or less than total amount of template and bigger than zero 
            if (templateNumber <= templateCount && templateNumber > 0)
            {
                /// Loads template by index
                LoadTemplate(templateNumber);
            }
        }

        /// <summary>
        /// Sets up new template
        /// </summary>
        internal void SetUpNewTemplate()
        {
            /// Sets temp name of the template 
            currentTemplate = "New Template";
            
            /// Sets temp name to UI
            UICreation.Instance.SetTemplate(currentTemplate);
            
            /// Checks avaliable button 
            CheckButtons();
        }

        /// <summary>
        /// Returns total number of templates in the folder
        /// </summary>
        private int GetNumberOfTemplates()
        {
            /// Sets path to the local variable
            string templateFolder = GetPath();

            /// Loads folder to the local variable
            DirectoryInfo dir = new DirectoryInfo(templateFolder);
            
            /// Loads all .xml files to the array
            FileInfo[] info = dir.GetFiles("*.xml");
            
            /// Assigns total number of files to the local variable
            int fileCount = info.Length;

            /// Clears array
            Array.Clear(info, 0, info.Length);

            /// Returns total number of templates
            return fileCount;
        }

        /// <summary>
        /// Returns path of the folder where template are stored. 
        /// </summary>
        private string GetPath()
        {
            /// Result local variable
            string result = "";

            /// Checks if project runs on mac
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                /// Assings path of the mac folder to the local variable
                result = "file://" + Application.dataPath + "/Mahjong Game Editor/Templates/";
            }
            else
            {
                /// Assings path of the folder to the local variable
                result = Application.dataPath + "/Mahjong Game Editor/Templates/";
            }

            return result;
        }

        /// <summary>
        /// Saves template to the folder
        /// </summary>
        internal void SaveTemplate()
        {
            /// Check if it is new template
            if (currentTemplate == "New Template")
            {
                /// Increase amount of templates
                templateCount++;
                
                /// Sets amount of templates to the current template index
                currentTemplateIndex = templateCount;
                
                /// Sets the name of the current template
                currentTemplate = "template" + currentTemplateIndex.ToString();
            }

            /// XmlSerializer variable
            var serializer = new XmlSerializer(typeof(Template));
            
            /// Creates FileStream according to path and name of the template
            var stream = new FileStream(GetPath() + currentTemplate + ".xml", FileMode.Create);
            
            /// Saves to the folder 
            serializer.Serialize(stream, new Template { tiles = GetTiles(), sequence = CreationTiles.Instance.sequence });
            
            /// Close stream
            stream.Close();

            /// Updates UI template name
            UICreation.Instance.SetTemplate("Template " + currentTemplateIndex);
            
            /// Updates UI amount of template
            UICreation.Instance.SetTemplatesCount(currentTemplateIndex, templateCount);
            
            /// Check for avaliable buttons
            CheckButtons();
        }

        /// <summary>
        /// Loads template by its index
        /// </summary>
        internal void LoadTemplate(int templateIndex)
        {
            /// Sets the current template index
            currentTemplateIndex = templateIndex;
            
            /// Sets the name of the current template index
            currentTemplate = "template" + templateIndex;

            /// XmlSerializer variable
            var serializer = new XmlSerializer(typeof(Template));

            /// Creates FileStrean according to path and the name of the template
            var stream = new FileStream(GetPath() + currentTemplate + ".xml", FileMode.Open);

            /// Loads file and transformate it to the Template class
            var container = serializer.Deserialize(stream) as Template;

            /// Sets the template 
            CreationTiles.Instance.SetTiles(container);

            /// Updates UI template name
            UICreation.Instance.SetTemplate("Template " + currentTemplateIndex);

            /// Update UI active amount of tiles
            UICreation.Instance.UpdateTileCount(CreationTiles.Instance.GetNumberOfOccupiedTiles());

            /// Updates UI amount of template
            UICreation.Instance.SetTemplatesCount(currentTemplateIndex, templateCount);

            /// Checks avaliable buttons
            CheckButtons();

            /// close stream
            stream.Close();
        }

        /// <summary>
        /// Checks conditions to activate editor buttons
        /// </summary>
        private void CheckButtons()
        {
            ///Checks if current template index is equal to amount of templates or equal to zero
            if (currentTemplateIndex == templateCount || currentTemplateIndex == 0)
            {
                /// Diactivates next button of the editor
                UICreation.Instance.DisableNextButton(true);
            }
            else
            {
                /// Activates next button of the editor
                UICreation.Instance.DisableNextButton(false);
            }

            ///Checks if current template index is equal to 1 or equal to zero
            if (currentTemplateIndex == 1 || currentTemplateIndex == 0)
            {

                /// Diactivates prev button of the editor
                UICreation.Instance.DisablePrevButton(true);
            }
            else
            {
                /// Activates prev button of the editor
                UICreation.Instance.DisablePrevButton(false);
            }

            /// Checks if amount of the templates is equal to zero
            if (templateCount == 0)
            {
                /// Diactivates Find Button of the editor
                UICreation.Instance.DisableFindButton(true);

                /// Diactivates Find First Button of the editor
                UICreation.Instance.DisableFindFirstButton(true);

                /// Diactivates Find Last Button of the editor
                UICreation.Instance.DisableFindLastButton(true);
            }
            else
            {
                /// Activates Find Button of the editor
                UICreation.Instance.DisableFindButton(false);

                /// Activates Find First Button of the editor
                UICreation.Instance.DisableFindFirstButton(false);

                /// Activates Find Last Button of the editor
                UICreation.Instance.DisableFindLastButton(false);
            }

            /// Checks if its unsaved new template 
            if (currentTemplate == "New Template")
            {
                /// Diactivate Load Button of the editor
                UICreation.Instance.DisableLoadButton(true);
                
                /// Checks even number of the tiles
                UICreation.Instance.CheckEvenNumber();
            }
            else
            {
                /// Activates Load Button of the editor
                UICreation.Instance.DisableLoadButton(false);
            }
        }

        /// <summary>
        /// Returns list of the tiles that will be saved
        /// </summary>
        internal List<XMLTile> GetTiles()
        {
            /// Assigns the list of the creation tile to the local variable
            List<CreationTile> creationTiles = CreationTiles.Instance.tiles;
            
            /// Creates empty list of XML tiles
            List<XMLTile> xmlTiles = new List<XMLTile>();

            /// Runs loop
            for (int i = 0; i < creationTiles.Count; i++)
            {
                /// Create local variable of XML tile
                XMLTile xmlTile = new XMLTile();
                
                /// Sets the id of xmlTile to creation tile id
                xmlTile.id = creationTiles[i].id;

                /// Sets the layer status of xmlTile to layer status of creation tile
                xmlTile.layer1status = creationTiles[i].layer1Status;

                /// Sets the layer status of xmlTile to layer status of creation tile
                xmlTile.layer2status = creationTiles[i].layer2Status;

                /// Sets the layer status of xmlTile to layer status of creation tile
                xmlTile.layer3status = creationTiles[i].layer3Status;

                /// Sets the layer status of xmlTile to layer status of creation tile
                xmlTile.layer4status = creationTiles[i].layer4Status;

                /// Sets the layer status of xmlTile to layer status of creation tile
                xmlTile.layer5status = creationTiles[i].layer5Status;
                
                /// Adds xmlTile to the list of xmlTiles
                xmlTiles.Add(xmlTile);
            }

            return xmlTiles;
        }
    }

    [XmlRoot]
    public class Template
    {
        [XmlArray("Tiles")]
        [XmlArrayItem("Tile")]
        public List<XMLTile> tiles { get; set; }

        [XmlArray("Sequence")]
        public List<string> sequence { get; set; }
    }

    public class XMLTile
    {
        [XmlAttribute("id")]
        public string id;
        [XmlAttribute("layer1Status")]
        public string layer1status;
        [XmlAttribute("layer2Status")]
        public string layer2status;
        [XmlAttribute("layer3Status")]
        public string layer3status;
        [XmlAttribute("layer4Status")]
        public string layer4status;
        [XmlAttribute("layer5Status")]
        public string layer5status;
    }
}