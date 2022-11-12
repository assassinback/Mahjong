// Author: Oleksii Stepanov

using UnityEngine;

namespace MahjongTemplateEditor
{
	/// <summary>
	/// Controls the position of the tile pointer
	/// </summary>
	internal class TilePointer : MonoBehaviour
	{
		internal static TilePointer Instance;

		/// <summary>
		/// SpriteRenderer of the object
		/// </summary>
		private SpriteRenderer spriteRenderer;

		/// <summary>
		/// Start position of the object
		/// </summary>
		private Vector3 startPos;

		/// <summary>
		/// Creates singleton of the object
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
			/// Assigns startPositon of the object
			startPos = transform.position;

			/// Assign SpriteRenderer to spriteRenderer variable
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// Sets the new position of the object
		/// </summary>
		internal void SetTargetPos(Transform target)
		{
			/// Sets the position of the object to target position 
			transform.position = target.position;
			
			/// Change SpriteRenderer sortingLayerName to target sortingLayerName
			spriteRenderer.sortingLayerName = target.GetComponent<SpriteRenderer>().sortingLayerName;

			/// Change SpriteRenderer sortingLayerOrder to target sortingLayerOrder +1
			spriteRenderer.sortingOrder = target.GetComponent<SpriteRenderer>().sortingOrder + 1;
		}

		/// <summary>
		/// Returns object to it's start position 
		/// </summary>
		internal void ResetPointer()
		{
			/// Sets object position to the start position 
			transform.position = startPos;
		}
	}
}