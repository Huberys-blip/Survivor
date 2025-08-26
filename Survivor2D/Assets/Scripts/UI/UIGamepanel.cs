using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectSurvicor;
using System;

namespace Script
{
	public class UIGamepanelData : UIPanelData
	{
	}
	public partial class UIGamepanel : UIPanel, IController
	{
		public static EasyEvent FlashScreen = new();
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamepanelData ?? new UIGamepanelData();
			Global.Coin.RegisterWithInitValue(coin =>
			{
				CionText.text = "金币" + coin;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
			{
				EnemyCountText.text = "敌人" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpValue.fillAmount = exp / (float)Global.ExpToNextLevel();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "等级" + level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			ExpUpgradePanel.Hide();
			Global.Level.Register(level =>
			{
				Time.timeScale = 0;
				AudioKit.PlaySound("LevelUp");
				ExpUpgradePanel.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if (exp >= Global.ExpToNextLevel())
				{
					Global.Exp.Value -= Global.ExpToNextLevel();
					Global.Level.Value++;
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.CuhrrentSeconds.RegisterWithInitValue(Seconds =>
			{
				if (Time.frameCount % 30 == 0)
				{
					var currentSecondsInt = Mathf.FloorToInt(Seconds);
					var seconds = currentSecondsInt % 60;
					var minutes = currentSecondsInt / 60;
					TimeText.text = "时间" + $"{minutes:00}:{seconds:00}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			var enemyGenerator = FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CuhrrentSeconds.Value += Time.deltaTime;
				if (enemyGenerator.LastWave && enemyGenerator.CurrentWave == null && EnemyGenerator.EnemyCount.Value == 0)
				{
					this.CloseSelf();
					UIKit.OpenPanel<UIGamePasspanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Coin.Register(coin =>
			{
				PlayerPrefs.SetInt("coin", coin);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			this.GetSystem<CoinUpgradeSystem>().Say();
			FlashScreen.Register(() =>
			{
				ActionKit.Sequence()
				.Lerp(0, 0.5f, 0.1f, alpha =>
				{
					ScreenColor.ColorAlpha(alpha);
				})
				.Lerp(0.5f, 0, 0.2f, alpha =>
				{
					ScreenColor.ColorAlpha(alpha);
				},()=>ScreenColor.ColorAlpha(0)).Start(this);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}
