using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectSurvicor;
using UnityEngine.SceneManagement;

namespace Script
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			// please add init code here

			BtnStartGame.onClick.AddListener(() =>
			{
				this.CloseSelf();
				SceneManager.LoadScene("Game");
			});
			BtnCoinUpgrade.onClick.AddListener(() =>
			{
				CoinUpgradePanel.Show();
			});
			
		
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
