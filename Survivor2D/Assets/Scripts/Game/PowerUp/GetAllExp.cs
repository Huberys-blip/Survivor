using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class GetAllExp : GamePlayObject
	{
		protected override Collider2D Collider2D => SelfCircleCollider2D;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<CollectableArea>())
			{
				foreach (var item in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
				{
					ActionKit.OnUpdate.Register(() =>
					{
						var plyaer = Player.Instance;
						if (plyaer != null)
						{
							var distance = plyaer.Position() - item.Position();
							item.transform.Translate(distance.normalized * Time.deltaTime * 5f);
						}
					}).UnRegisterWhenGameObjectDestroyed(item);
				}
				AudioKit.PlaySound("GetAllExp");
				this.DestroyGameObjGracefully();
			}
		}
	}
}
