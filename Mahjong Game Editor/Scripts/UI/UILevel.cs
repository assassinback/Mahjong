// Author: Oleksii Stepanov

using UnityEngine;
using UnityEngine.UI;

namespace MahjongTemplateEditor
{
	/// <summary>
	/// Class that controls level panel
	/// </summary>
	internal class UILevel : MonoBehaviour
	{
		internal static UILevel Instance;

		/// <summary>
		/// Animator of the Control Panel
		/// </summary>
		[SerializeField] private Animator animControlPanel;

		/// <summary>
		/// Animator of the Template
		/// </summary>
		[SerializeField] private Animator animTemplate;

		/// <summary>
		/// To Editor Button
		/// </summary>
		[SerializeField] private Button toEditorButton;

		/// <summary>
		/// Start Button - Show group of tiles
		/// </summary>
		[SerializeField] private Button startButton;

		/// <summary>
		/// Hint Button - Highlights Possible combination
		/// </summary>
		[SerializeField] private Button hintButton;

		/// <summary>
		/// Bomb Button - Removes combination
		/// </summary>
		[SerializeField] private Button bombButton;

		/// <summary>
		/// Shuffle Button - Shuffles the template
		/// </summary>
		[SerializeField] private Button shuffleButton;

		/// <summary>
		/// Text that shows naming of current template
		/// </summary>
		[SerializeField] private Text currentTemplate;

		/// <summary>
		/// Text that shows total amount of templates
		/// </summary>
		[SerializeField] private Text templateCountText;

		/// <summary>
		/// Current Template Index
		/// </summary>
		[SerializeField] private int currentTemplateInt = 0;

		/// <summary>
		/// Current amount of templates
		/// </summary>
		[SerializeField] private int templatesCount = 0;

		/// <summary>
		/// InputField for finding templates
		/// </summary>
		[SerializeField] private InputField findInputField;

		/// <summary>
		/// Find Button - Finds Template according to text of the InputField
		/// </summary>
		[SerializeField] private Button findButton;

		/// <summary>
		/// Next Button - Sets next possible template
		/// </summary>
		[SerializeField] private Button nextButton;

		/// <summary>
		/// Previous Button - Sets previous possible templates
		/// </summary>
		[SerializeField] private Button prevButton;

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
			/// Assigns FindTemplate method to FindButton
			findButton.onClick.AddListener(() => FindTemplate());
			
			/// Assigns ShowTemplate method to FindButton
			findButton.onClick.AddListener(() => ShowTemplate(false));

			/// Assigns NextTexmplate method to NextButton 
			nextButton.onClick.AddListener(() => NextTemplate());

			/// Assigns ShowTemplate method to NextButton 
			nextButton.onClick.AddListener(() => ShowTemplate(false));

			/// Assigns PrevTemplate method to PrevButton
			prevButton.onClick.AddListener(() => PrevTemplate());

			/// Assigns ShowTemplate method to PrevButton
			prevButton.onClick.AddListener(() => ShowTemplate(false));

			/// Assigns ShowTemplate method to ToEditorButton
			toEditorButton.onClick.AddListener(() => ShowTemplate(false));

			/// Assigns ShowToEditorButton method to ToEditorButton
			toEditorButton.onClick.AddListener(() => ShowToEditorButton(false));

			/// Assigns ShowControlPanel method to ToEditorButton
			toEditorButton.onClick.AddListener(() => ShowControlPanel(false));

			/// Assigns ShowControlPanel method of the UICreation to ToEditorButton
			toEditorButton.onClick.AddListener(() => UICreation.Instance.ShowControlPanel(true));

			/// Assigns ShowFieldPanel method of the UICreation to ToEditorButton
			toEditorButton.onClick.AddListener(() => UICreation.Instance.ShowFieldPanel(true));

			/// Assigns ShowInfoPanel method of the UICreation to ToEditorButton
			toEditorButton.onClick.AddListener(() => UICreation.Instance.ShowInfoPanel(true));

			/// Assigns ShowToGameButton method of the UICreation to ToEditorButton
			toEditorButton.onClick.AddListener(() => UICreation.Instance.ShowToGameButton(true));

			/// Assigns ShowCreationTiles method of the UICreation to ToEditorButton
			toEditorButton.onClick.AddListener(() => UICreation.Instance.ShowCreationTiles(true));
			
			/// Assigns StartLevel method to StartButton
			startButton.onClick.AddListener(() => StartLevel());
			
			/// Assigns HighlightTwoTiles method of the TemplateManager class to HintButton
			hintButton.onClick.AddListener(() => TemplateManager.Instance.HightlightTwoTiles());

			/// Assigns RemoveTwoTiles method of the TemplateManager class to BombButton
			bombButton.onClick.AddListener(() => TemplateManager.Instance.RemoveTwoTiles());

			/// Assigns Shuffle method of the TemplateManager class to ShuffleButton
			shuffleButton.onClick.AddListener(() => TemplateManager.Instance.Shuffle());
		}

		/// <summary>
		/// Sets a template by index
		/// </summary>
		private void SetTemplate(int index)
		{
			/// Assigns index to the currentTemplateInt
			currentTemplateInt = index;
			
			/// Sets an index to the UIText 
			currentTemplate.text = "Template " + index;

			/// Sets an index to the UIText as well as template count
			templateCountText.text = currentTemplateInt + "/" + templatesCount;
		}

		/// <summary>
		/// Reads information from inputField, checks if template exists and then sets template
		/// </summary>
		private void FindTemplate()
		{
			/// Assign content of the findInputField to local variable and also convert it to int
			int templateIndex = int.Parse(findInputField.text);

			/// Check if template exists
			if (IsTemplateExists(templateIndex)) 
			{
				///Sets template by it's index
				SetTemplate(templateIndex);
			}
		}

		/// <summary>
		/// Checks if increased by one template is exists and then sets template
		/// </summary>
		private void NextTemplate()
		{
			/// Assigns value of currentTemplateInt + 1 to local variable
			int templateIndex = currentTemplateInt + 1;

			/// Check if template exists
			if (IsTemplateExists(templateIndex))
			{
				///Sets template by it's index
				SetTemplate(templateIndex);
			}
		}

		/// <summary>
		/// Checks if increased by one template is exists and then sets template
		/// </summary>
		private void PrevTemplate()
		{
			/// Assigns value of currentTemplateInt - 1 to local variable
			int templateIndex = currentTemplateInt - 1;

			/// Check if template exists
			if (IsTemplateExists(templateIndex))
			{
				///Sets template by it's index
				SetTemplate(templateIndex);
			}
		}

		/// <summary>
		/// Method that checks if template exists by its index
		/// </summary>
		private bool IsTemplateExists(int templateIndex)
		{

			/// Runs loop to check template index
			for (int i = 0; i < templatesCount; i++)
			{
				/// Check if template index is exists
				if (templateIndex == i)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Method that updates panel after template was loaded 
		/// </summary>
		internal void UpdatePanel(int currentTemplateValue, int templatesCountValue)
		{
			/// Check is template count value is bigger the 0
			if (templatesCountValue > 0)
			{
				/// Sets the template by index
				SetTemplate(currentTemplateValue);

				/// Assings templateCound variable with current value
				templatesCount = templatesCountValue;

				/// Sets the text of current template index and total number of templates
				templateCountText.text = currentTemplateInt + "/" + templatesCount;

				/// Unblocks all buttons
				BlockAllButtons(false);
			}
			else
			{
				/// Sets currentTemplate text to No Templates
				currentTemplate.text = "No Templates";
				
				/// Blacks every button
				BlockAllButtons(true);
			}
		}

		/// <summary>
		/// Method that blocks or unblocks panels buttons
		/// </summary>
		private void BlockAllButtons(bool value)
		{
			/// Sets interactable value of the StartButton
			startButton.interactable = !value;

			/// Sets interactable value of the FindButton
			findButton.interactable = !value;

			/// Sets interactable value of the NextButton
			nextButton.interactable = !value;

			/// Sets interactable value of the PrevButton
			prevButton.interactable = !value;

			/// Sets interactable value of the BombButton
			bombButton.interactable = !value;

			/// Sets interactable value of the ShuffleButton
			shuffleButton.interactable = !value;

			/// Sets interactable value of the HintButton
			hintButton.interactable = !value;
		}

		/// <summary>
		/// Method that shows/hides ControlPanel
		/// </summary>
		internal void ShowControlPanel(bool value)
		{
			/// Sets animators variable Show to the values
			animControlPanel.SetBool("Show", value);
		}

		/// <summary>
		/// Method that shows/hides Template
		/// </summary>
		internal void ShowTemplate(bool value)
		{
			/// Sets animators variable Show to the values
			animTemplate.SetBool("Show", value);
		}

		/// <summary>
		/// Method thats loads template
		/// </summary>
		private void StartLevel()
		{
			/// Loads template by index of the template
			TemplateManager.Instance.SetUp(currentTemplateInt);
		}

		/// <summary>
		/// Method that show/hides EditorButton
		/// </summary>
		internal void ShowToEditorButton(bool value)
		{
			/// Sets active status of the button to value
			toEditorButton.gameObject.SetActive(value);
		}
	}
}