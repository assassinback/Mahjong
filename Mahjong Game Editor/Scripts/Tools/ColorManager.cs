// Author: Oleksii Stepanov

using UnityEngine;

namespace MahjongTemplateEditor
{
	/// <summary>
	/// Holds useful colors
	/// </summary>
	internal class ColorManager : MonoBehaviour
	{
		internal static ColorManager Instance;

		[SerializeField] internal Color black;
		[SerializeField] internal Color red;
		[SerializeField] internal Color blue;
		[SerializeField] internal Color pink;
		[SerializeField] internal Color orange;
		[SerializeField] internal Color green;
		[SerializeField] internal Color lightRed;
		[SerializeField] internal Color lightBlue;
		[SerializeField] internal Color lightPink;
		[SerializeField] internal Color lightOrange;
		[SerializeField] internal Color lightGreen;

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
	}
}