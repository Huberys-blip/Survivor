using UnityEngine;
using QFramework;
using Unity.Burst.Intrinsics;

namespace Script
{
	public partial class SimpleCircle : ViewController
	{
		void Start()
		{
			// Code Here
			Circle.OnTriggerEnter2DEvent(collider =>
			{
				var hurtbox = collider.GetComponent<HurtBox>();
				if (hurtbox)
				{
					if (hurtbox.Owner.CompareTag("Enemy"))
					{
						hurtbox.Owner.GetComponent<Enemy>().Hurt(2);
					}
				}
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		private void Update()
		{
			var radius = 3;
			var degree = Time.frameCount;
			var circleLocalPos = new Vector2(-Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)) * radius;
			Circle.LocalPosition(circleLocalPos.x,circleLocalPos.y);
        }
    }
}
