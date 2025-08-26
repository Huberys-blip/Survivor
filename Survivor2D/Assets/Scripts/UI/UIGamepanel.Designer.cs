using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	// Generate Id:ee5a4db0-f45b-464c-a5db-2482a528aaab
	public partial class UIGamepanel
	{
		public const string Name = "UIGamepanel";
		
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public UnityEngine.UI.Text CionText;
		[SerializeField]
		public ExpUpgradePanel ExpUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Image ExpValue;
		[SerializeField]
		public UnityEngine.UI.Image ScreenColor;
		
		private UIGamepanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			LevelText = null;
			TimeText = null;
			EnemyCountText = null;
			CionText = null;
			ExpUpgradePanel = null;
			ExpValue = null;
			ScreenColor = null;
			
			mData = null;
		}
		
		public UIGamepanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamepanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamepanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
