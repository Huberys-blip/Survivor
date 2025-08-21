using UnityEngine;
using QFramework;
using ProjectSurvicor;
using System.Linq;

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
			if (mCurrentSeconds >= 1)
			{
				// var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				// foreach (var enemy in enemies)
				// {
				// 	var distance = (Player.Instance.transform.position - enemy.transform.position).magnitude;
				// 	if (distance <= 5)
				// 	{
				// 		enemy.Hurt(2);
				// 	}
				// }
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				var enemy = enemies.OrderBy(enemy=>(Player.Instance.transform.position-enemy.transform.position).magnitude).FirstOrDefault();
				if (enemy)
				{
					Knife.Instantiate()
					.Position(this.Position())
					.Show()
					.Self(self =>
					{
						var rigidbody2D = self.GetComponent<Rigidbody2D>();
						var direction = (enemy.Position() - Player.Instance.Position());
						rigidbody2D.velocity = direction * 10;
						self.OnTriggerEnter2DEvent(collider =>
						{
							var hurtbox = collider.GetComponent<HurtBox>();
							if (hurtbox)
							{
								if (hurtbox.Owner.CompareTag("Enemy"))
								{
									hurtbox.Owner.GetComponent<Enemy>().Hurt(5);
								}
							}
						}).UnRegisterWhenGameObjectDestroyed(self);
						ActionKit.OnUpdate.Register(() =>
				     	{
						if ((Player.Instance.Position() - self.Position()).magnitude> 20)
						{
							self.DestroyGameObjGracefully();
						}
					    }).UnRegisterWhenGameObjectDestroyed(self);
					});
				}
				mCurrentSeconds = 0;
			}
		}
    }
}
