using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class Hp : ViewController
	{
		  private void OnTriggerEnter2D(Collider2D collision)
		    {
				if (collision.GetComponent<CollectableArea>())
				{
					if (Global.hp.Value < Global.MaxHp.Value)
					{
						AudioKit.PlaySound("Hp");
						Global.hp.Value++;
						this.DestroyGameObjGracefully();
					}

				}
		    }
		
	}
}
