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
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamepanelData ?? new UIGamepanelData();
			// please add init code here
			Global.hp.RegisterWithInitValue(hp =>
			{
				HpText.text = "生命值" + $"{hp}/{Global.MaxHp.Value}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.MaxHp.RegisterWithInitValue(maxHp =>
			{
				HpText.text = "生命值" + $"{Global.hp.Value}/{maxHp}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
			{
				EnemyCountText.text = "敌人" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "经验值" + exp + "/" + Global.ExpToNextLevel();
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
