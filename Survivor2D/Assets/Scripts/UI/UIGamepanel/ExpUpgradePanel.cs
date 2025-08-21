/****************************************************************************
 * 2025.8 蜘蛛
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class ExpUpgradePanel : UIElement,IController
	{
		private void Awake()
		{
			var expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			foreach (var expUpgradItem in expUpgradeSystem.items)
			{
				BtnExpUpgtadeitemTemplate.InstantiateWithParent(UpgradeRoot).Self(self =>
				{
					var itemCache = expUpgradItem;
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
						
							selfCache.Show();
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
		}

        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }
    }
}