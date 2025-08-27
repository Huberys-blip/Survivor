using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class Coin : GamePlayObject
	{
		protected override Collider2D Collider2D => SelfCircleCollider2D;
		  private void OnTriggerEnter2D(Collider2D collision)
        {
			if (collision.GetComponent<CollectableArea>())
			{
				AudioKit.PlaySound("Coin");
				Global.Coin.Value++;
				this.DestroyGameObjGracefully();
			}
        }
	}
}
