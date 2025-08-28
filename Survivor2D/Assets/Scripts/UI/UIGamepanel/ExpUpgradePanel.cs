/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectSurvicor;
using UnityEngine.U2D;

namespace Script
{
	public partial class ExpUpgradePanel : UIElement,IController
	{
		ResLoader mResLoader;
		private void Awake()
		{
			 mResLoader = ResLoader.Allocate();
            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
			//var simpleKnifeIcon = iconAtlas.GetSprite("093_"+"paired_bomb_icon_0");
			//Debug.Log(iconAtlas.spriteCount);
			// var sprites = new Sprite[iconAtlas.spriteCount];
            // iconAtlas.GetSprites(sprites);
			// foreach (var sprite in sprites)
			// {
			// 	Debug.Log(sprite.name);
			// }
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			foreach (var expUpgradItem in expUpgradeSystem.items)
			{
				BtnExpUpgtadeitemTemplate.InstantiateWithParent(UpgradeRoot).Self(self =>
				{
					var itemCache = expUpgradItem;
					self.transform.Find("Icon").GetComponent<Image>().sprite=iconAtlas.GetSprite(itemCache.IconName);
					self.onClick.AddListener(() =>
					{
						Time.timeScale = 1;
						AudioKit.PlaySound("AbilityleveUp");
						itemCache.Upgrade();
						this.Hide();
					});
					var selfCache = self;
					itemCache.Visible.RegisterWithInitValue((visible) =>
					{
						if (visible)
						{
							self.GetComponentInChildren<Text>().text =
							expUpgradItem.Descriptopn;
							selfCache.Show();
							var pairedUpgradename =selfCache.transform.Find("PairedUpgradeName");
							if (expUpgradeSystem.Pairs.TryGetValue(itemCache.Key, out var pairedName))
							{
								var pairedItem = expUpgradeSystem.Dictionary[pairedName];
								if (pairedItem.CurrentLeve.Value > 0 && itemCache.CurrentLeve.Value == 0)
								{
									pairedUpgradename.Find("Icon").GetComponent<Image>().sprite = iconAtlas.GetSprite(pairedItem.PairedIconName);
									pairedUpgradename.Show();
								}
								else
								{
									pairedUpgradename.Hide();
								}
							}
							else
							{
								pairedUpgradename.Hide();
							}
						}

						else
						{
							selfCache.Hide();
						}
					}).UnRegisterWhenGameObjectDestroyed(selfCache);		
					itemCache.CurrentLeve.RegisterWithInitValue(lv=>
					{
							selfCache.GetComponentInChildren<Text>().text = itemCache.Descriptopn;
					}).UnRegisterWhenGameObjectDestroyed(selfCache);	
				});
			}
		

		}

		protected override void OnBeforeDestroy()
		{
			//self.transform.Find("Icon").GetComponent<Image>().sprite=simpleKnifeIcon;
			mResLoader.Recycle2Cache();
			mResLoader = null;
		}

        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }
    }
}