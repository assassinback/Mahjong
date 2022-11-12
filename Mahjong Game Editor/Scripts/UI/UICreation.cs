// Author: Oleksii Stepanov

using UnityEngine;
using UnityEngine.UI;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Class that controls panels of the editor 
    /// </summary>
    internal class UICreation : MonoBehaviour
    {
        internal static UICreation Instance;

        /// <summary>
        /// Animator of the object
        /// </summary>
        private Animator anim;

        /// <summary>
        /// Animator of the Field Panel
        /// </summary>
        [SerializeField] private Animator animField;

        /// <summary>
        /// Animator of the Control Panel
        /// </summary>
        [SerializeField] private Animator animControl;

        /// <summary>
        /// Animator of the Info Panel
        /// </summary>
        [SerializeField] private Animator animInfo;

        /// <summary>
        /// GameObject that holds all Creation Tiles
        /// </summary>
        [SerializeField] private GameObject creationTiles;

        /// <summary>
        /// GameObject that shows center of the field
        /// </summary>
        [SerializeField] private GameObject center;

        /// <summary>
        /// Number of the tiles
        /// </summary>
        private int tileCountValue = 0;

        /// <summary>
        /// UIText that shows current template text
        /// </summary>
        [SerializeField] private Text currentTemplateText;

        /// <summary>
        /// UIText that shows text of the action
        /// </summary>
        [SerializeField] private Text actionText;

        /// <summary>
        /// UIText that shows number of tiles
        /// </summary>
        [SerializeField] private Text tileCount;

        /// <summary>
        /// UIText that shows combination
        /// </summary>
        [SerializeField] private Text combinationCount;

        /// <summary>
        /// UIText that shows number of the templates
        /// </summary>
        [SerializeField] private Text templatesCount;

        /// <summary>
        /// Button that creates a empty template
        /// </summary>
        [SerializeField] private Button newTemplateButton;

        /// <summary>
        /// Button that saves template
        /// </summary>
        [SerializeField] private Button saveButton;

        /// <summary>
        /// Button that loads template
        /// </summary>
        [SerializeField] private Button loadButton;

        /// <summary>
        /// Button that cleans field
        /// </summary>
        [SerializeField] private Button cleanButton;

        /// <summary>
        /// Button that loads game menu
        /// </summary>
        [SerializeField] private Button toGameButton;

        /// <summary>
        /// InputField for index of the template for search
        /// </summary>
        [SerializeField] private InputField findInputField;

        /// <summary>
        /// Button that finds template by text in the InputField
        /// </summary>
        [SerializeField] private Button findButton;

        /// <summary>
        /// Button that loads next template
        /// </summary>
        [SerializeField] private Button nextButton;

        /// <summary>
        /// Button that loads previous template
        /// </summary>
        [SerializeField] private Button prevButton;

        /// <summary>
        /// Button that loads first template
        /// </summary>
        [SerializeField] private Button findFirstButton;

        /// <summary>
        /// Button that loads last template
        /// </summary>
        [SerializeField] private Button findLastButton;

        /// <summary>
        /// Current template Index
        /// </summary>
        private int templateCurrentInt;

        /// <summary>
        /// Current number of the templates
        /// </summary>
        private int templateCountValue;

        /// <summary>
        /// Creation of the singleton
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
            /// Assigns Animator of the object to the variable
            anim = GetComponent<Animator>();

            /// Hides To Editor Button of the UILevel
            UILevel.Instance.ShowToEditorButton(false);
            
            /// Hide Control Panel of the UILevel
            UILevel.Instance.ShowControlPanel(false);

            /// Shows Control Panel
            ShowControlPanel(true);
            
            /// Shows Field Panel
            ShowFieldPanel(true);
            
            /// Shows Info Panel
            ShowInfoPanel(true);
            
            /// Shows ToGame button
            ShowToGameButton(true);
            
            /// Shows creation tiles
            ShowCreationTiles(true);

            /// Assing CreateNeTemplate method to NewTemplateButton
            newTemplateButton.onClick.AddListener(() => CreateNewTemplate());

            /// Assing SaveTemplate method to SaveButton
            saveButton.onClick.AddListener(() => SaveTemplate());

            /// Assing LoadTemplate method to LoadButton
            loadButton.onClick.AddListener(() => LoadTemplate());

            /// Assing CleanTemplate method to CleanButton
            cleanButton.onClick.AddListener(() => CleanTemplate());

            /// Assing FindTemplate method to FindButton
            findButton.onClick.AddListener(() => FindTemplate());

            /// Assing NextTemplate method to NextButton
            nextButton.onClick.AddListener(() => NextTemplate());

            /// Assing PrevTemplate method to PrevButton
            prevButton.onClick.AddListener(() => PrevTemplate());

            /// Assing LoadFirstTemplate method of the SaveTemplateManager to FindFirstButton
            findFirstButton.onClick.AddListener(() => SaveTemplateManager.Instance.LoadFirstTemplate());

            /// Assing LoadLastTemplate method of the SaveTemplateManager to FindLastButton
            findLastButton.onClick.AddListener(() => SaveTemplateManager.Instance.LoadLastTemplate());

            /// Assing FindTemplate method to FindButton
            findButton.onClick.AddListener(() => FindTemplate());

            /// Assing ToGame method to ToGameButton
            toGameButton.onClick.AddListener(() => ToGame());
        }

        /// <summary>
        /// Method that hides Editor view and shows Game View
        /// </summary>
        private void ToGame()
        {
            /// Updates Panel of the UILevel
            UILevel.Instance.UpdatePanel(templateCurrentInt, templateCountValue);

            /// Hides creation tiles
            ShowCreationTiles(false);
            
            /// Hides ToGame button 
            ShowToGameButton(false);
            
            /// Hides Control Panel
            ShowControlPanel(false);
            
            /// Hides Field Panel
            ShowFieldPanel(false);
            
            /// Hides Info Pane;
            ShowInfoPanel(false);

            /// Shows Controls Panel
            UILevel.Instance.ShowControlPanel(true);
            
            /// Shows To Editor Button;
            UILevel.Instance.ShowToEditorButton(true);
        }

        /// <summary>
        /// Method that shows/hides control panel
        /// </summary>
        internal void ShowControlPanel(bool value)
        {
            animControl.SetBool("Show", value);
        }

        /// <summary>
        /// Method that shows/hides toGameButton
        /// </summary>
        internal void ShowToGameButton(bool value)
        {
            toGameButton.gameObject.SetActive(value);
        }

        /// <summary>
        /// Method that shows/hides creation tiles
        /// </summary>
        internal void ShowCreationTiles(bool value)
        {
            /// Shows/Hides creation tiles
            creationTiles.gameObject.SetActive(value);
            
            /// Shows/Hide center
            center.gameObject.SetActive(value);
        }

        /// <summary>
        /// Method that shows/hides Field Panel
        /// </summary>
        internal void ShowFieldPanel(bool value)
        {
            animField.SetBool("Show", value);
        }

        /// <summary>
        /// Method that shows/hides info panel
        /// </summary>
        internal void ShowInfoPanel(bool value)
        {
            animInfo.SetBool("Show", value);
        }

        /// <summary>
        /// Method that set template text to the currentTemplateText object
        /// </summary>
        internal void SetTemplate(string currentTemplate)
        {
            currentTemplateText.text = currentTemplate;
        }

        /// <summary>
        /// Method that saves template 
        /// </summary>
        private void SaveTemplate()
        {
            /// Set text to the action text object 
            actionText.text = "Saving Completed";
            
            /// Activates animator of the actionAnimator 
            anim.SetTrigger("ShowActionText");
            
            /// Enables Save Template Method of the SaveTemplateManager
            SaveTemplateManager.Instance.SaveTemplate();
        }

        /// <summary>
        /// Method that loads template 
        /// </summary>
        private void LoadTemplate()
        {
            /// Set text to the action text object 
            actionText.text = "Loading Completed";

            /// Activates animator of the actionAnimator 
            anim.SetTrigger("ShowActionText");

            /// Enables Load Template Method of the SaveTemplateManager
            SaveTemplateManager.Instance.LoadTemplate(templateCurrentInt);
        }

        /// <summary>
        /// Method that take index of the template of the findInputField and share it with SaveTemplate class to get Template
        /// </summary>
        private void FindTemplate()
        {
            /// Parse findInputFieldText to int and then assigns it to local variable
            int templateIndex = int.Parse(findInputField.text);
            
            /// Loads Template 
            SaveTemplateManager.Instance.LoadTemplate(templateIndex);
        }

        /// <summary>
        /// Method that creates new template
        /// </summary>
        private void CreateNewTemplate()
        {
            /// Reset number of the active tiles
            tileCountValue = 0;
            
            /// Sets number of the active tiles to the tileCount UIText
            tileCount.text = "Tiles: " + tileCountValue.ToString();
            
            /// Sets combination text
            combinationCount.text = "Combinations: 0";

            /// Set text to the action text object 
            actionText.text = "New Template! Good Luck!";

            /// Activates animator of the actionAnimator 
            anim.SetTrigger("ShowActionText");

            /// Enables SetUpNewTemplate method of the SaveTemplateManager class
            SaveTemplateManager.Instance.SetUpNewTemplate();
            
            /// Clear all tiles of the editor
            CreationTiles.Instance.Clean();
        }

        /// <summary>
        /// Loads next template 
        /// </summary>
        private void NextTemplate()
        {
            /// Set text to the action text object 
            actionText.text = "Next Template Loaded";

            /// Activates animator of the actionAnimator 
            anim.SetTrigger("ShowActionText");

            /// Loads next template
            SaveTemplateManager.Instance.LoadNextTemplate(true);
        }

        /// <summary>
        /// Loads previous template
        /// </summary>
        private void PrevTemplate()
        {
            /// Set text to the action text object 
            actionText.text = "Previous Template Loaded";

            /// Activates animator of the actionAnimator 
            anim.SetTrigger("ShowActionText");

            /// Loads previous template
            SaveTemplateManager.Instance.LoadNextTemplate(false);
        }

        /// <summary>
        /// Method that resets field of the creation tiles 
        /// </summary>
        private void CleanTemplate()
        {
            /// Sets number of the tiles to 0
            tileCountValue = 0;
            
            /// Updates number of tile
            UpdateTileCount(tileCountValue);

            /// Reset number of the active tiles
            tileCountValue = 0;

            /// Sets number of the active tiles to the tileCount UIText
            tileCount.text = "Tiles: " + tileCountValue.ToString();

            /// Sets combination text
            combinationCount.text = "Combinations: 0";

            /// Set text to the action text object 
            actionText.text = "Template Cleaned";

            /// Activates animator of the actionAnimator
            anim.SetTrigger("ShowActionText");

            /// Resets list of the creation tiles 
            CreationTiles.Instance.Clean();
        }

        /// <summary>
        /// Adds one tile to the number of tiles
        /// </summary>
        internal void AddTileCount()
        {
            /// Adds one tile to the number of tiles
            tileCountValue++;

            /// Updates number of tiles 
            UpdateTileCount(tileCountValue);
        }

        /// <summary>
        /// Reduces one tile from the number of tiles
        /// </summary>
        internal void RemoveTileCount()
        {
            /// Reduces one tile from the number of tiles
            tileCountValue--;

            /// Updates number of tiles 
            UpdateTileCount(tileCountValue);
        }

        /// <summary>
        /// Checks if tiles are even number and bigger the 0
        /// </summary>
        internal void CheckEvenNumber()
        {
            /// Checks if tiles are even number and bigger the 0
            if (tileCountValue % 2 == 0 && tileCountValue != 0)
            {
                /// Sets number of combinations to UIText
                combinationCount.text = "Combinations: " + (tileCountValue / 2).ToString();
                
                /// Enables Save button
                saveButton.interactable = true;
            }
            else
            {
                /// Disables SaveButton
                saveButton.interactable = false;
            }
        }

        /// <summary>
        /// Update number of tiles
        /// </summary>
        internal void UpdateTileCount(int value)
        {
            /// Sets the number of tile to UIText
            tileCount.text = "Tiles: " + value.ToString();
            
            /// Updates number of tiles
            tileCountValue = value;

            /// Checks if number of tile are even number and begger that 0
            CheckEvenNumber();
        }

        /// <summary>
        /// Enables/Disables next template button
        /// </summary>
        internal void DisableNextButton(bool value)
        {
            nextButton.interactable = !value;
        }

        /// <summary>
        /// Enables/Disables previous template button
        /// </summary>
        internal void DisablePrevButton(bool value)
        {
            prevButton.interactable = !value;
        }

        /// <summary>
        /// Enables/Disables load template button
        /// </summary>
        internal void DisableLoadButton(bool value)
        {
            loadButton.interactable = !value;
        }

        /// <summary>
        /// Enables/Disables find template button
        /// </summary>
        internal void DisableFindButton(bool value)
        {
            findButton.interactable = !value;
        }

        /// <summary>
        /// Enables/Disables find last template button
        /// </summary>
        internal void DisableFindLastButton(bool value)
        {
            findLastButton.interactable = !value;
        }

        /// <summary>
        /// Enables/Disables find first template button
        /// </summary>
        internal void DisableFindFirstButton(bool value)
        {
            findFirstButton.interactable = !value;
        }

        /// <summary>
        /// Sets number of the templates 
        /// </summary>
        internal void SetTemplatesCount(int current, int max)
        {
            /// Assigns current active template to templateCurrentInt 
            templateCurrentInt = current;
            
            /// Assigns total number of templates to templateCountValue
            templateCountValue = max;
            
            /// Sets current active template and total number of templates to UIText
            templatesCount.text = current + "/" + max;
        }
    }
}