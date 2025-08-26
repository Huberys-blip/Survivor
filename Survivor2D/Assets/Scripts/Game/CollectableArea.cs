using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class CollectableArea : ViewController
	{
		void Start()
		{
			Global.CollectableArea.RegisterWithInitValue(range =>
			{
				GetComponent<CircleCollider2D>().radius = range;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
	}
}
