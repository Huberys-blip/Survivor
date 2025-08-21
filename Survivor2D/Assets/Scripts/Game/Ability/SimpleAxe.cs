using UnityEngine;
using QFramework;
using Unity.Burst.Intrinsics;

namespace Script
{
	public partial class SimpleAxe : ViewController
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
				Axe.Instantiate()
				.Show()
				.Position(this.Position())
				.Self(self =>
				{
					var rigidbody2D = self.GetComponent<Rigidbody2D>();
					var randomX = RandomUtility.Choose(-8, -5, -3, 3, 5, 8);
					var randomY = RandomUtility.Choose(5, 8);
					rigidbody2D.velocity = new Vector2(randomX, randomY);
					self.OnTriggerEnter2DEvent(collider =>
					{
						var hurtbox = collider.GetComponent<HurtBox>();
						if (hurtbox)
						{
							if (hurtbox.Owner.CompareTag("Enemy"))
							{
								hurtbox.Owner.GetComponent<Enemy>().Hurt(2);
							}
						}
					}).UnRegisterWhenGameObjectDestroyed(self);
					ActionKit.OnUpdate.Register(() =>
					{
						if (Player.Instance.Position().y - self.Position().y > 20)
						{
							self.DestroyGameObjGracefully();
						}
					}).UnRegisterWhenGameObjectDestroyed(self);
				});
				mCurrentSeconds =0;
			}
        }
    }
}