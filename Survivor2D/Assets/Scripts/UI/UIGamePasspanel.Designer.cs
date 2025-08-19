using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	// Generate Id:3b83e416-7c38-4874-af77-f2cbc4b8b120
	public partial class UIGamePasspanel
	{
		public const string Name = "UIGamePasspanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnBackToStart;
		
		private UIGamePasspanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnBackToStart = null;
			
			mData = null;
		}
		
		public UIGamePasspanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePasspanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePasspanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
