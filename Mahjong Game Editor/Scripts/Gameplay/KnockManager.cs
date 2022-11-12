// Author: Oleksii Stepanov

using UnityEngine;

namespace MahjongTemplateEditor {

	/// <summary>
	/// Class that takes two gameobject and imitatites knocking animation
	/// </summary>
	internal class KnockManager : MonoBehaviour
	{
		/// <summary>
		/// First GameObject that will be knocked;
		/// </summary>
		private GameObject tile1;

		/// <summary>
		/// Second GameObject that will be knocked;
		/// </summary>
		private GameObject tile2;

		/// <summary>
		/// Center point between two GameObjects
		/// </summary>
		private Vector3 Center;

		/// <summary>
		/// Final position for GameObject 1
		/// </summary>
		private Vector3 targetForTile1;

		/// <summary>
		/// Final position for GameObject 2
		/// </summary>
		private Vector3 targetForTile2;

		/// <summary>
		/// Side position for GameObject 1 (Left)
		/// </summary>
		private Vector3 sideTargetForTile1;

		/// <summary>
		/// Side position for GameObject 1 (Right)
		/// </summary>
		private Vector3 sideTargetForTile2;

		/// <summary>
		/// General movement flag 
		/// </summary>
		private bool move = false;

		/// <summary>
		/// Movement to sides flag
		/// </summary>
		private bool moveToSides = false;

		/// <summary>
		/// Movement to center flag
		/// </summary>
		private bool moveToCenter = false;

		/// <summary>
		/// Step of the movement
		/// </summary>
		private float step = 0;

		/// <summary>
		/// Speed of the movement
		/// </summary>
		private float speed = 4f;

		/// <summary>
		/// Set up two tiles for knocking movement
		/// </summary>
		internal void SetUp(GameObject tile1Value, GameObject tile2Value, int sortingLayerValue)
		{
			/// Creates copy of the tile 1
			GameObject tile1Object = Instantiate(tile1Value);

			/// Creates copy of the tile 2
			GameObject tile2Object = Instantiate(tile2Value);

			/// Sets the same position as tile 1 for copy 
			tile1Object.transform.position = tile1Value.transform.position;

			/// Sets the same position as tile 2 for copy
			tile2Object.transform.position = tile2Value.transform.position;

			/// Sets the parent of the copy 1 to the current gameObject
			tile1Object.transform.parent = transform.parent;

			/// Sets the parent of the copy 2 to the current gameObject
			tile2Object.transform.parent = transform.parent;

			/// Check if the tile 1 is on the right to the tile 2 and then assigns gameObjects
			if (tile1Value.transform.position.x >= tile2Value.transform.position.x)
			{
				tile1 = tile1Object;
				tile2 = tile2Object;
			}
			else
			{
				tile1 = tile2Object;
				tile2 = tile1Object;
			}

			/// Disables origin tile 1
			tile1Value.SetActive(false);

			/// Disables origin tile 2
			tile2Value.SetActive(false);

			/// Creates target for movement
			SetUpTargets();

			/// Sets the right sorting layer
			SetUpSortingLayer(tile1, sortingLayerValue + 1);

			/// Sets the right sorting layer
			SetUpSortingLayer(tile2, sortingLayerValue + 2);

			/// Enables general movement flag
			move = true;

			/// Enables side movement flag
			moveToSides = true;
		}

		/// <summary>
		/// Sets the sorting layer to Knock layer and sets order in it
		/// </summary>
		private void SetUpSortingLayer(GameObject tile, int order) {
			/// Assigns SpriteRenderer of the object to the local variable
			SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();

			/// Sets SpriteRenderer sorting layer
			spriteRenderer.sortingLayerName = "Knock";

			/// Sets SpriteRenderer sorting layer order
			tile2.GetComponent<SpriteRenderer>().sortingOrder = order;
		}

		/// <summary>
		/// Sets target position for tiles movement
		/// </summary>
		private void SetUpTargets() {
			/// Finds center between to tiles 
			Center = GetCenterPosition(tile1.transform.position, tile2.transform.position);

			/// Find center position of the tile 1
			targetForTile1 = Center + new Vector3(0.8f / 2, 0, 0);

			/// Find center position of the tile 2
			targetForTile2 = Center + new Vector3(-0.8f / 2, 0, 0);

			/// Find side position of the tile 1
			sideTargetForTile1 = new Vector3(tile1.transform.position.x + 1, targetForTile1.y, tile1.transform.position.z);

			/// Find side position of the tile 2
			sideTargetForTile2 = new Vector3(tile2.transform.position.x - 1, targetForTile2.y, tile2.transform.position.z);
		}

		/// <summary>
		/// Returns center position between two points
		/// </summary>
		private Vector3 GetCenterPosition(Vector3 pos1, Vector3 pos2)
		{
			return Vector3.Lerp(pos1, pos2, 0.5f);
		}

		private void FixedUpdate()
		{
			/// Checks if general movement flag is true
			if (move)
			{
				/// Updates movement step variable
				step += Time.deltaTime * speed;

				/// Checks if side movement flag is true
				if (moveToSides)
				{
					/// Creates movement to sides
					MoveToSides();
				}

				/// Checks if center movement flag is true
				if (moveToCenter)
				{
					/// Creates movement to center
					MoveToCenter();
				}
			}
		}

		/// <summary>
		/// Moves two tiles to the side target
		/// </summary>
		private void MoveToSides() 
		{
			/// Moves tile 1 to the it's side target by step
			tile1.transform.position = Vector3.Slerp(tile1.transform.position, sideTargetForTile1, step);

			/// Moves tile 2 to the it's side target by step
			tile2.transform.position = Vector3.Slerp(tile2.transform.position, sideTargetForTile2, step);

			/// Check if step is quel to 0.4 (half of the movement)
			if (step >= 0.4f)
			{
				/// Sets step to zero
				step = 0;

				/// Diactivates movement to the sides 
				moveToSides = false;

				/// Activates movement to the center
				moveToCenter = true;
			}
		}

		/// <summary>
		/// Moves to tile to the center target
		/// </summary>
		private void MoveToCenter() 
		{
			/// Moves tile 1 to the it's center target by step
			tile1.transform.position = Vector3.Slerp(tile1.transform.position, targetForTile1, step);

			/// Moves tile 2 to the it's center target by step
			tile2.transform.position = Vector3.Slerp(tile2.transform.position, targetForTile2, step);

			/// Check if step is quel to 0.6 (half of the movement)
			if (step >= 0.6f)
			{
				/// Stops moving
				ResetAll();
			}
		}

		/// <summary>
		/// Resets all logical variables of the class and Destroys it
		/// </summary>
		private void ResetAll()
		{
			/// Diactivates general movement
			move = false;

			/// Diactivates center movement
			moveToCenter = false;

			/// Sets step to zero
			step = 0;

			/// Adds TileLayerHidder class to tile 1
			tile1.AddComponent<TileLayerHidder>();

			/// Adds TileLayerHidder class to tile 2
			tile2.AddComponent<TileLayerHidder>();
			
			/// Sets tile 1 GameObject to null
			tile1 = null;

			/// Sets tile 2 GameObject to null
			tile2 = null;

			/// Destroys class
			Destroy(this);
		}
	}
}
