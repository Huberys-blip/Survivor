using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class Bomb : GamePlayObject
	{
		public static void Execute()
		{
			
			foreach (var item in FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
			{
				DamageSystem.CalculatDamage(Global.BombDamage.Value,item);
			}
			AudioKit.PlaySound("Bomb");
			UIGamepanel.FlashScreen.Trigger();
			CameraController.Shake();
		}
		protected override Collider2D Collider2D => SelfCircleCollider2D;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<CollectableArea>())
			{
				Execute();
				this.DestroyGameObjGracefully();
			}
		}
	}
}
