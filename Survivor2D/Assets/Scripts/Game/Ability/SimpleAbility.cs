using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class SimpleAbility : ViewController
	{
		private float mCurrentSeconds = 0f;
		void Start()
		{
			// Code Here
		}
		void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)
			{
				mCurrentSeconds = 0f;
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var enemy in enemies)
				{
					var distance = (Player.Instance.transform.position - enemy.transform.position).magnitude;
					if (distance <= 5)
					{
						enemy.Hurt(Global.SimpleAbilityDamage.Value);
					}
				}
			}
		}
	}
}
