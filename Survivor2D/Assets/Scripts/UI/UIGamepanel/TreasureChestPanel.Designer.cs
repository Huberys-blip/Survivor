/****************************************************************************
 * 2025.8 AF的铁疙瘩
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	public partial class TreasureChestPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnSure;
		[SerializeField] public UnityEngine.UI.Text Content;

		public void Clear()
		{
			BtnSure = null;
			Content = null;
		}

		public override string ComponentName
		{
			get { return "TreasureChestPanel";}
		}
	}
}
