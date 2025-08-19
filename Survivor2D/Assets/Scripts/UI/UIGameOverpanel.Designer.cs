using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Script
{
	// Generate Id:501b9c02-ef8d-47f0-b823-41c9e18ded91
	public partial class UIGameOverpanel
	{
		public const string Name = "UIGameOverpanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnBackToStart;
		
		private UIGameOverpanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnBackToStart = null;
			
			mData = null;
		}
		
		public UIGameOverpanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameOverpanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameOverpanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
