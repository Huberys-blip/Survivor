using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class Exp : PowerUp
	{

   protected override Collider2D Collider2D => SelfCircleCollider2D;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<CollectableArea>())
			{
				FlyingPlayer = true;
			}
		}

        protected override void Execute()
        {
            AudioKit.PlaySound("Exp");
			Global.Exp.Value++;
			this.DestroyGameObjGracefully();
        }
    }
	
}
