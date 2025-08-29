/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	public partial class UnlockedlconPanel
	{
		[SerializeField] public UnityEngine.UI.Image UnlockedlconTemplate;
		[SerializeField] public RectTransform UnlockedlconRoot;

		public void Clear()
		{
			UnlockedlconTemplate = null;
			UnlockedlconRoot = null;
		}

		public override string ComponentName
		{
			get { return "UnlockedlconPanel";}
		}
	}
}
