using UnityEngine;
using QFramework;
using ProjectSurvicor;
using System.Linq;

namespace Script
{
	public partial class AbilityController : ViewController, IController
	{

		void Start()
		{
			Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleKnife.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.RotateSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					RotateSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.BasketBallUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					BasketBallAility.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//随机升级一个
			this.GetSystem<ExpUpgradeSystem>().items.Where(item => item.IsWeapon).ToList().GetRandomItem().Upgrade();
			Global.SuperBomb.RegisterWithInitValue(unlocker =>
			{
				if (unlocker)
				{
					SuperBomb.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		 public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }

	}
}
