/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/
using UnityEngine.UI;
using QFramework;
using ProjectSurvicor;
using System;
using System.Linq;

namespace Script
{
	public partial class CoinUpgradePanel : UIElement, IController
	{
		private void Awake()
		{
			CoinUpgtafeltemTemplate.Hide();
			BtnClose.onClick.AddListener(() =>
			{
				this.Hide();
			});	

			foreach (var item in this.GetSystem<CoinUpgradeSystem>().items.Where(it=>!it.UpgradeFinish))
			{
				CoinUpgtafeltemTemplate.InstantiateWithParent(CoinUpgradeitemRoot).Self(self =>
				{
					var itemCache = item;

					self.GetComponentInChildren<Text>().text = item.Descriptopn + $"{item.Price}金币";
					self.onClick.AddListener(() =>
					{
						AudioKit.PlaySound("AbilityleveUp");
						itemCache.Upgrade();
					});
					var selfCache = self;
					item.OnChanged.Register(() =>
					{
						if (itemCache.CounditionCheck())
						{
							selfCache.Show();
						}
						else
						{
							selfCache.Hide();
						}
					}).UnRegisterWhenGameObjectDestroyed(selfCache);
					if (itemCache.CounditionCheck())
					{
						selfCache.Show();
					}
					else
					{
						selfCache.Hide();
					}
					Global.Coin.RegisterWithInitValue(coin =>
					{
						if (coin > itemCache.Price)
						{
							selfCache.interactable = true;
						}
						else
						{
							selfCache.interactable = false;
						}
						CoinText.text = "金币：" + coin;
					}).UnRegisterWhenGameObjectDestroyed(self);
				});
			}
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}