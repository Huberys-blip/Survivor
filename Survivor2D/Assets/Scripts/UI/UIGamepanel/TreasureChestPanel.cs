/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/
using UnityEngine;
using QFramework;
using ProjectSurvicor;
using System.Linq;
using QAssetBundle;
using UnityEngine.U2D;

namespace Script
{
	public partial class TreasureChestPanel : UIElement,IController
	{
		ResLoader mResLoader = ResLoader.Allocate();
		private void Awake()
		{
			BtnSure.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				this.Hide();
			});
		}
		private void OnEnable()
		{
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			var matchedPairedItems= expUpgradeSystem.items.Where(item =>
			{
				 if (item.CurrentLeve.Value >= 7)
				// if (item.CurrentLeve.Value >= 1&&item.PairedName.IsNotNullAndEmpty())
				{
					var containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
					var pairedItemKey = expUpgradeSystem.Pairs[item.Key];
					var PairedItemStartUpgrade = expUpgradeSystem.Dictionary[pairedItemKey].CurrentLeve.Value > 0;
					var pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;
					return containsInPair && PairedItemStartUpgrade && !pairedUnlocked;
				}
				return false;
			});
			if (matchedPairedItems.Any())
			{
				var item = matchedPairedItems.ToList().GetRandomItem();
				Content.text = "<b>" + "合成后的" + item.Name + "</b>\n";
				while (!item.UpgradeFinish)
				{
					item.Upgrade();
				}
			Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("Icon").GetSprite(item.PairedIconName);
				Icon.Show();
				expUpgradeSystem.PairedProperties[item.Key].Value = true;
			}
			else
			{
				var upgradeItems = expUpgradeSystem.items.Where(item => item.CurrentLeve.Value > 0 && !item.UpgradeFinish).ToList();
				if (upgradeItems.Any())
				{
					var item = upgradeItems.GetRandomItem();
					Content.text = item.Descriptopn;
					Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("Icon").GetSprite(item.IconName);
					Icon.Show();
					item.Upgrade();

				}
				else
				{
					if (Global.hp.Value < Global.MaxHp.Value)
					{
						if (Random.Range(0, 1.0f) < 0.2f)
						{
							Icon.Hide();
							Content.text = "恢复 1 血量";
							AudioKit.PlaySound(Sfx.HP);
							Global.hp.Value++;
							return;
						}
					}
					Content.text = "增加50金币";
					Global.Coin.Value += 50;
					Icon.Hide();
				}
			}
		
		}

		protected override void OnBeforeDestroy()
		{
			mResLoader.Recycle2Cache();
			mResLoader = null;
		}

        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }
    }
}