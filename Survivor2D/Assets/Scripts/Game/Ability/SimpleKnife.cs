using UnityEngine;
using QFramework;
using ProjectSurvicor;
using System.Linq;
using QAssetBundle;

namespace Script
{
	public partial class SimpleKnife : ViewController
	{
		void Start()
		{

		}
		private float mCurrentSeconds = 0;
		private void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= Global.SimpleKnifeDuration.Value)
			{
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
				.OrderBy(enemy => Player.Instance.Distance2D(enemy))
				.Take(Global.SimpleKnifeCount.Value+Global.AdditionalFlyThingCount.Value);
				var i = 0;
				foreach (var enemy in enemies)
				{
					if (enemy)
					{
						if (i<4)
						{
							ActionKit.DelayFrame(11 * i, () =>
							{
								AudioKit.PlaySound(Sfx.KNIFE);
							}).StartGlobal();
							i++;
						}
						
						Knife.Instantiate()
						.Position(this.Position())
						.Show()
						.Self(self =>
						{
							
							var SelfCache = self;
							var direction = enemy.NormalizedDirection2DFrom(Player.Instance);
							self.transform.up = direction;
							var rigidbody2D = self.GetComponent<Rigidbody2D>();
							rigidbody2D.velocity = direction * 10;
							self.OnTriggerEnter2DEvent(collider =>
							{
								var hurtbox = collider.GetComponent<HurtBox>();
								if (hurtbox)
								{
									if (hurtbox.Owner.CompareTag("Enemy"))
									{
										DamageSystem.CalculatDamage(Global.SimpleKnifeDamage.Value,hurtbox.Owner.GetComponent<Enemy>());
										//hurtbox.Owner.GetComponent<Enemy>().Hurt(Global.SimpleKnifeDamage.Value);
										// SelfCache.DestroyGameObjGracefully();
									}
								}
							}).UnRegisterWhenGameObjectDestroyed(self);
							ActionKit.OnUpdate.Register(() =>
							 {
								 if (Player.Instance.Distance2D(SelfCache) > 20)
								 {
									 self.DestroyGameObjGracefully();
								 }
							 }).UnRegisterWhenGameObjectDestroyed(self);
						});
					}
				}
				mCurrentSeconds = 0;
			}
		}
    }
}
