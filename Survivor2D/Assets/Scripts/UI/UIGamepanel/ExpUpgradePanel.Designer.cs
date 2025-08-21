/****************************************************************************
 * 2025.8 AF的铁疙瘩
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

		public void Clear()
		{
			UpgradeRoot = null;
			BtnExpUpgtadeitemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
