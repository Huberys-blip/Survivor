/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public RectTransform UpgradeRoot;
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgtadeitemTemplate;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			UpgradeRoot = null;
			BtnExpUpgtadeitemTemplate = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
