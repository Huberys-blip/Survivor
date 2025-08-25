using UnityEngine;
using QFramework;
using Unity.Burst.Intrinsics;
using ProjectSurvicor;
using System.Collections.Generic;

namespace Script
{
	public partial class RotateSword : ViewController
	{
		List<Collider2D> mSwords = new();
		void Start()
		{
			Sword.Hide();
			Global.RotateSwordCount.RegisterWithInitValue(count =>
			{
				var toAddCount = count - mSwords.Count;
				for (int i = 0; i < toAddCount; i++)
				{
					mSwords.Add(Sword.InstantiateWithParent(this)
					.Self(self =>
					{
						self.OnTriggerEnter2DEvent(collider =>
						{
							var hurtbox = collider.GetComponent<HurtBox>();
							if (hurtbox)
							{
								if (hurtbox.Owner.CompareTag("Enemy"))
								{
									hurtbox.Owner.GetComponent<Enemy>().Hurt(Global.RotateSwordDamage.Value);
									if (Random.Range(0, 1.0f) < 0.5f)
									{
										collider.attachedRigidbody.velocity = collider.NormalizedDirection2DFrom(self)*5+collider.NormalizedDirection2DFrom(Player.Instance)*10;
									}
								}
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
					}).Show());
				}
				UPdateCirclePos();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.RotateSwordRange.Register((range) =>
			{
				UPdateCirclePos();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

		}
		void UPdateCirclePos()
		{
			var radius = Global.RotateSwordRange.Value;
			var durationDegrees = 360 / mSwords.Count;
			for (int i = 0; i < mSwords.Count; i++)
			{
				var circleLocalPos = new Vector2(Mathf.Cos(durationDegrees * i * Mathf.Deg2Rad), Mathf.Sin(durationDegrees * i * Mathf.Deg2Rad)) * radius;
				mSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
			.LocalEulerAnglesZ(durationDegrees * i - 90);
			}

		}
		private void Update()
		{

			var degree = Time.frameCount * Global.RotateSwordSpeed.Value;
			this.LocalEulerAnglesZ(-degree);

		}
	}
}
