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
			BtnClose.onClick.AddListener(() =>
			{
				CoinUpgradePanel.Hide();
			});
			Global.Coin.RegisterWithInitValue(coin=>
			{
				CoinText.text = "金币：" + coin;
				if (coin >= 5)
				{
					BtnCoinPercentUpgrade.Show();
					BtnExpPercentUpgrade.Show();
				}
				else
				{
					BtnCoinPercentUpgrade.Hide();
					BtnExpPercentUpgrade.Hide();
				}
				if (coin >= 30)
				{
					BtnPlayerMaxHpUpgrade.Show();
				}
				else
				{
					BtnPlayerMaxHpUpgrade.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			BtnCoinPercentUpgrade.onClick.AddListener(() =>
			{
				AudioKit.PlaySound("AbilityleveUp");
				Global.Coin.Value-=5;
				Global.CoinPercent.Value+=0.1f;
			});
			BtnExpPercentUpgrade.onClick.AddListener(() =>
			{
				AudioKit.PlaySound("AbilityleveUp");
				Global.Coin.Value-=5;
				Global.ExpPercent.Value+=0.1f;
			});
			BtnPlayerMaxHpUpgrade.onClick.AddListener(() =>
			{
				AudioKit.PlaySound("AbilityleveUp");
				Global.Coin.Value -= 30;
				Global.MaxHp.Value++;
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
