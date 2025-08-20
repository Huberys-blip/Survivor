/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public RectTransform CoinUpgradeitemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinUpgtafeltemTemplate;

		public void Clear()
		{
			CoinText = null;
			BtnClose = null;
			CoinUpgradeitemRoot = null;
			CoinUpgtafeltemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
