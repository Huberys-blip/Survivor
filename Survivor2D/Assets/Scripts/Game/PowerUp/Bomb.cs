using UnityEngine;
using QFramework;

namespace Script
{
	public partial class Bomb : ViewController
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<CollectableArea>())
			{
				
				foreach (var item in FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
				{
					item.Hurt(item.hp);
				}
				AudioKit.PlaySound("Bomb");
				CameraController.Shake();
				this.DestroyGameObjGracefully();
			}
		}
	}
}
