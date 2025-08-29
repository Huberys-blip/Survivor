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
	public partial class UnlockedlconPanel : UIElement, IController
	{
		private Dictionary<string, System.Tuple<ExpUpgradeltem, Image>> mUnlockedKeys = new();
		ResLoader mResLoader = ResLoader.Allocate();
		private void Awake()
		{
			UnlockedlconTemplate.Hide();
			var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
			foreach (var expUpgradeItem in this.GetSystem<ExpUpgradeSystem>().items)
			{
				var cacjeditem = expUpgradeItem;
				expUpgradeItem.CurrentLeve.RegisterWithInitValue(leve =>
				{
					if (leve > 0)
					{
						if (mUnlockedKeys.ContainsKey(cacjeditem.Key))
						{

						}
						else
						{
							UnlockedlconTemplate.InstantiateWithParent(UnlockedlconRoot).Self(self =>
							{
								self.sprite = iconAtlas.GetSprite(cacjeditem.PairedIconName);
								mUnlockedKeys.Add(cacjeditem.Key, new System.Tuple<ExpUpgradeltem, Image>(cacjeditem, self));
								// Debug.LogAssertion(cacjeditem.Key);
							}).Show();
						}
					}
				}).UnRegisterWhenGameObjectDestroyed(gameObject);
			}
			Global.SuperKnife.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnlockedKeys.ContainsKey("simple_knife_icon"))
					{
						var item = mUnlockedKeys["simple_knife_icon"].Item1;
						var sprite = iconAtlas.GetSprite(item.PairedName);
						mUnlockedKeys["simple_knife_icon"].Item2.sprite = sprite;
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.SuperRotateSword.Register(unlocked =>
		{
			if (unlocked)
			{
				if (mUnlockedKeys.ContainsKey("rotate_sword_icon"))
				{
					var item = mUnlockedKeys["rotate_sword_icon"].Item1;
					var sprite = iconAtlas.GetSprite(item.PairedIconName);
					mUnlockedKeys["rotate_sword_icon"].Item2.sprite = sprite;
				}
			}
		}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.SuperBasketBall.Register(unlocked =>
		{
			if (unlocked)
			{
				if (mUnlockedKeys.ContainsKey("ball_icon"))
				{
					var item = mUnlockedKeys["ball_icon"].Item1;
					var sprite = iconAtlas.GetSprite(item.PairedIconName);
					mUnlockedKeys["ball_icon"].Item2.sprite = sprite;
				}
			}
		}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.SuperBomb.Register(unlocked =>
	{
		if (unlocked)
		{
			if (mUnlockedKeys.ContainsKey("bomb_icon"))
			{
				var item = mUnlockedKeys["bomb_icon"].Item1;
				var sprite = iconAtlas.GetSprite(item.PairedIconName);
				mUnlockedKeys["bomb_icon"].Item2.sprite = sprite;
			}
		}
	}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.SuperSword.Register(unlocked =>
{
	if (unlocked)
	{
		if (mUnlockedKeys.ContainsKey("simple_sword_icon"))
		{
			var item = mUnlockedKeys["simple_sword_icon"].Item1;
			var sprite = iconAtlas.GetSprite(item.PairedIconName);
			mUnlockedKeys["simple_sword_icon"].Item2.sprite = sprite;
		}
	}
}).UnRegisterWhenGameObjectDestroyed(gameObject);
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