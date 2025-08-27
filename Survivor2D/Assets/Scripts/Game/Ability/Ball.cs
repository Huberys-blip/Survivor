using UnityEngine;
using QFramework;
using ProjectSurvicor;
using QAssetBundle;

namespace Script
{
	public partial class Ball : ViewController
	{
		void Start()
		{
			SelfRigidbody2D.velocity =
			new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *
			Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2);
			Global.SuperBasketBall.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					this.LocalScale(3);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			HurtBox.OnTriggerEnter2DEvent(Collider =>
			{
				var hurtBox = Collider.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						var enemy = hurtBox.Owner.GetComponent<IEnemy>();
						var damageTiems = Global.SuperBasketBall.Value ? Random.Range(2, 3 + 1) : 1;
						DamageSystem.CalculatDamage(Global.BasketBallDamage.Value*damageTiems,enemy);
						if (Random.Range(0, 1.0f) < 0.5f&&Collider.attachedRigidbody&&Player.Instance)
						{
							Collider.attachedRigidbody.velocity = Collider.NormalizedDirection2DFrom(this) * 5 + Collider.NormalizedDirection2DFrom(Player.Instance)  * 10;
						}
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		private void OnCollisionEnter2D(Collision2D collision)
		{
			var normal = collision.GetContact(0).normal;
			if (normal.x > normal.y)
			{
				SelfRigidbody2D.velocity = new Vector2(SelfRigidbody2D.velocity.x, Mathf.Sign(SelfRigidbody2D.velocity.y)
				* Random.Range(0.5f, 1.5f) * Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2));
				SelfRigidbody2D.angularVelocity = Random.Range(-360, 360);
			}
			else
			{
				var rb = SelfRigidbody2D;
				rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * Random.Range(0.5f, 1.5f) * Random.Range(
					Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2), rb.velocity.y);
				rb.angularVelocity = Random.Range(-360, 360);
			}
			AudioKit.PlaySound(Sfx.BALL);
		}
	}
}
