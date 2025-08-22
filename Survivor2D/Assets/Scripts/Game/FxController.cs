using UnityEngine;
using QFramework;

namespace ProjectSurvicor
{
	public partial class FxController : ViewController
	{
		public static FxController Instance;
		void Awake()
		{
			Instance = this;
		}
		void OnDestroy()
		{
			Instance = null;
		}
		public static void Play(SpriteRenderer sprite, Color dissoceColor)
		{
			Instance.EnemyDieFx.Instantiate()
			.Position(sprite.Position())
			.LocalScale(sprite.Scale())
			.Self(s =>
			{
				s.GetComponent<Dissolve>().DissolveColor = dissoceColor;
				s.sprite = sprite.sprite;
			}).Show();
		}
    }
}
