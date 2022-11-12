// Author: Oleksii Stepanov

using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class that controls loading template from folder
    /// </summary>
    internal class XMLTemplateManager : MonoBehaviour
    {
        internal static XMLTemplateManager Instance;

        /// <summary>
        /// Current template variable
        /// </summary>
        private string currentTemplate = "";


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

        /// <summary>
        /// Loads template by index
        /// </summary>
        internal void LoadTemplate(string value)
        {
            /// template to load variable
            currentTemplate = "template" + value;

            /// Start Coroutine to load template from folder
            StartCoroutine(Load());
        }

        /// <summary>
        /// Loads template from folder
        /// </summary>
        private IEnumerator Load()
        {
            /// XmlSerializer variable
            XmlSerializer serializer = new XmlSerializer(typeof(Template));

            /// Loading xml file from folder
            using (UnityWebRequest request = UnityWebRequest.Get(GetPath() + currentTemplate + ".xml"))
            {
                /// Trying to create web request
                yield return request.SendWebRequest();

                /// Check if there is problem with finding file
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    /// Notification of the error
                    Debug.Log(request.error);
                }
                else
                { 
                    /// Text reader variable with all text of the xml file
                    TextReader textReader = new StringReader(request.downloadHandler.text);

                    /// Template Container of the deserialize xml file 
                    var container = serializer.Deserialize(textReader) as Template;

                    /// Send container to the TemplateManager 
                    TemplateManager.Instance.SetTemplate(container);

                    /// Close text reader
                    textReader.Close();
                }
            }
        }

        /// <summary>
        /// Return path of the folder according to platform 
        /// </summary>
        private string GetPath()
        {
            /// Result variable
            string result = "";

            /// Check if platform is MacOs
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                /// Assigns path to the result variable
                result = "file://" + Application.dataPath + "/Mahjong Game Editor/Templates/";
            }
            else
            {
                /// Assigns path to the result variable
                result = Application.dataPath + "/Mahjong Game Editor/Templates/";
            }

            return result;
        }
    }
}