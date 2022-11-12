// Author: Oleksii Stepanov

using System.Collections.Generic;
using UnityEngine;

namespace MahjongTemplateEditor
{
	/// <summary>
	/// Class that controls list of creation tiles
	/// </summary>
	internal class CreationTiles : MonoBehaviour
	{
		internal static CreationTiles Instance;

		/// <summary>
		/// List of creation tiles
		/// </summary>
		[SerializeField] internal List<CreationTile> tiles = new List<CreationTile>();

		/// <summary>
		/// List of the all tiles ids in order
		/// </summary>
		internal List<string> sequence = new List<string>();

		/// <summary>
		/// Creation of singletone
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
		/// Adds id of the tile to sequence
		/// </summary>
		internal void AddToSequence(string id)
		{
			///
			sequence.Add(id);
		}

		/// <summary>
		/// Updates one of the tiles from the with status of the layer
		/// </summary>
		internal void UpdateTile(string id, int layerNumber, string status)
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Checks if tile id is equels to provided id  
				if (tiles[i].id == id)
				{
					/// Checks if layer number equels to 1
					if (layerNumber == 1)
					{
						/// Sets the status of the layer 1 
						tiles[i].SetLayer1Status(status);

						/// Sets the color of tile
						tiles[i].SetColor();
					}
					/// Checks if layer number equels to 2
					else if (layerNumber == 2)
					{
						/// Sets the status of the layer 2
						tiles[i].SetLayer2Status(status);

						/// Sets the color of tile
						tiles[i].SetColor();
					}
					/// Checks if layer number equels to 3
					else if (layerNumber == 3)
					{
						/// Sets the status of the layer 3
						tiles[i].SetLayer3Status(status);

						/// Sets the color of tile
						tiles[i].SetColor();
					}
					/// Checks if layer number equels to 4
					else if (layerNumber == 4)
					{
						/// Sets the status of the layer 4
						tiles[i].SetLayer4Status(status);

						/// Sets the color of tile
						tiles[i].SetColor();
					}
					/// Checks if layer number equels to 5
					else if (layerNumber == 5)
					{
						/// Sets the status of the layer 5
						tiles[i].SetLayer5Status(status);

						/// Sets the color of tile
						tiles[i].SetColor();
					}
				}
			}
		}

		/// <summary>
		/// Blocks tile by id
		/// </summary>
		internal void BlockTile(string id)
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Checks if tile id is equels to provided id  
				if (tiles[i].id == id)
				{
					/// Blocks tile 
					tiles[i].BlockTile(true);
				}
			}
		}

		/// <summary>
		/// Unblocks all tiles
		/// </summary>
		internal void UnblockAll()
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Unblocks tile 
				tiles[i].BlockTile(false);
			}
		}

		/// <summary>
		/// Returns tile by id
		/// </summary>
		internal CreationTile GetTileById(string id)
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Checks if tile id is equels to provided id  
				if (tiles[i].id == id)
				{
					/// Returns tile
					return tiles[i];
				}
			}

			return null;
		}

		/// <summary>
		/// Cleans all tiles 
		/// </summary>
		internal void Clean()
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Sets layer 1 status to empty
				tiles[i].SetLayer1Status("empty");

				/// Sets layer 2 status to empty
				tiles[i].SetLayer2Status("empty");

				/// Sets layer 3 status to empty
				tiles[i].SetLayer3Status("empty");

				/// Sets layer 4 status to empty
				tiles[i].SetLayer4Status("empty");

				/// Sets layer 5 status to empty
				tiles[i].SetLayer5Status("empty");

				/// Sets color
				tiles[i].SetColor();
			}

			/// Clear sequence list
			sequence.Clear();
		}

		/// <summary>
		/// Sets up tiles list with template 
		/// </summary>
		internal void SetTiles(Template template)
		{
			/// Runs loop for all of the tiles 
			for (int i = 0; i < template.tiles.Count; i++)
			{
				/// Sets layer status 1 with template tile layer 1 status
				tiles[i].SetLayer1Status(template.tiles[i].layer1status);

				/// Sets layer status 2 with template tile layer 2 status
				tiles[i].SetLayer2Status(template.tiles[i].layer2status);

				/// Sets layer status 3 with template tile layer 3 status
				tiles[i].SetLayer3Status(template.tiles[i].layer3status);

				/// Sets layer status 4 with template tile layer 4 status
				tiles[i].SetLayer4Status(template.tiles[i].layer4status);

				/// Sets layer status 5 with template tile layer 5 status
				tiles[i].SetLayer5Status(template.tiles[i].layer5status);

				/// Sets color of the tile
				tiles[i].SetColor();
			}

			/// Sets sequence with template sequence
			SetSequence(template.sequence);
		}

		/// <summary>
		/// Sets sequence
		/// </summary>
		private void SetSequence(List<string> value)
		{
			/// Sets sequence with list of the strings
			sequence = value;
		}

		/// <summary>
		/// Returns number of occupied tiles
		/// </summary>
		internal int GetNumberOfOccupiedTiles()
		{
			/// Local variable
			int counter = 0;

			/// Runs loop for all of the tiles 
			for (int i = 0; i < tiles.Count; i++)
			{
				/// Check is layer 1 is occupied
				if (tiles[i].layer1Status == "occupied")
				{
					/// Updates counter
					counter++;
				}

				/// Check is layer 2 is occupied
				if (tiles[i].layer2Status == "occupied")
				{
					/// Updates counter
					counter++;
				}

				/// Check is layer 3 is occupied
				if (tiles[i].layer3Status == "occupied")
				{
					/// Updates counter
					counter++;
				}

				/// Check is layer 4 is occupied
				if (tiles[i].layer4Status == "occupied")
				{
					/// Updates counter
					counter++;
				}

				/// Check is layer 5 is occupied
				if (tiles[i].layer5Status == "occupied")
				{
					/// Updates counter
					counter++;
				}
			}

			return counter;
		}
	}
}