using UnityEngine;
using QFramework;
using ProjectSurvicor;
using QAssetBundle;

namespace Script
{
	public partial class TreasureChest : GamePlayObject
	{
		protected override Collider2D Collider2D => SelfCircleCollider2D;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<CollectableArea>())
			{
				UIGamepanel.OpenTreasurePanel.Trigger();
				AudioKit.PlaySound(Sfx.TREASUER_CHEST);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
