using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	// Generate Id:dab49cac-70f4-44a2-aea0-fab2a2073bae
	public partial class UIGamepanel
	{
		public const string Name = "UIGamepanel";
		
		[SerializeField]
		public UnityEngine.UI.Text HpText;
		[SerializeField]
		public UnityEngine.UI.Text ExpText;
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public UnityEngine.UI.Text CionText;
		[SerializeField]
		public RectTransform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleDurationUpgrade;
		
		private UIGamepanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HpText = null;
			ExpText = null;
			LevelText = null;
			TimeText = null;
			EnemyCountText = null;
			CionText = null;
			UpgradeRoot = null;
			BtnUpgrade = null;
			BtnSimpleDurationUpgrade = null;
			
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
