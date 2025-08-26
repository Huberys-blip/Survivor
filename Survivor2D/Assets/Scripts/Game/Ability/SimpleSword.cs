using UnityEngine;
using QFramework;
using ProjectSurvicor;
using System.Linq;

namespace Script
{
	public partial class SimpleSword : ViewController
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
				foreach (var enemy in enemies.OrderBy(e => e.Direction2DFrom(Player.Instance).magnitude)
				.Where(e => e.Direction2DFrom(Player.Instance).magnitude < Global.SimpleSwordRange.Value)
				.Take(Global.SimpleSwordConut.Value+Global.AdditionalFlyThingCount.Value))
				{
					
						Sword.Instantiate().Position(enemy.Position() + Vector3.left * 0.25f)
						.Show()
						.Self(self =>
						{
							var selfCache = self;
							selfCache.OnTriggerEnter2DEvent(Collider2D =>
							{
								var hurtBox = Collider2D.GetComponent<HurtBox>();
								if (hurtBox)
								{
									if (hurtBox.Owner.CompareTag("Enemy"))
									{
										if (enemy)
										{
											DamageSystem.CalculatDamage(Global.SimpleAbilityDamage.Value,hurtBox.Owner.GetComponent<Enemy>());
											//enemy.Hurt(Global.SimpleAbilityDamage.Value);
										}
										
									}
								}
							}).UnRegisterWhenGameObjectDestroyed(gameObject);
							//劈砍动画
							ActionKit.Sequence()
							.Callback((() => { selfCache.enabled = false; }))
							.Parallel(p =>
							{
								p.Lerp(0, 10, 0.2f, (z) =>
								{
									self.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
								});
								p.Append(
									ActionKit.Sequence().Lerp(0, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
									.Lerp(1.25f, 1, 0.1f, scale => selfCache.LocalScale(scale))
								);
							})
							.Callback(() => { selfCache.enabled = true; })
							.Parallel(p =>
							{
								p.Lerp(10, -180, 0.2f, z =>
								{
									selfCache.transform.LocalEulerAnglesZ(z);
									p.Append(
										ActionKit.Sequence().Lerp(1, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
									.Lerp(1.25f, 1, 0.1f, scale => selfCache.LocalScale(scale))
									);
								});
							})
							.Callback(() => { selfCache.enabled = false; })
							.Lerp(-180,0,0.3f,z=>
							{
								selfCache.transform.LocalEulerAnglesZ(z).LocalScale(z.Abs()/180);
							})
							.Start(this,() =>
							{
								selfCache.DestroyGameObjGracefully();
							});
						});
						
					
				}
			}
		}
	}
}
