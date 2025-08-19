using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	// Generate Id:5b56e864-0dd4-4c71-bf27-1605bee485ea
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnPlayerMaxHpUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnStartGame = null;
			CoinUpgradePanel = null;
			CoinText = null;
			BtnCoinPercentUpgrade = null;
			BtnExpPercentUpgrade = null;
			BtnPlayerMaxHpUpgrade = null;
			BtnClose = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
