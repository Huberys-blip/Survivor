using UnityEngine;
using QFramework;

namespace Script
{
	public partial class FloatingTextController : ViewController
	{
		static FloatingTextController Instance;
		private void Awake()
		{
			Instance = this;
		}
		private void OnDestroy()
		{
			Instance = null;
		}
		void Start()
		{
			FloatingText.Hide();
		}
		public static void Play(Vector2 position, string text,bool critical =false)
		{
			Instance.FloatingText.InstantiateWithParent(Instance.transform).PositionX(position.x).PositionY(position.y).Self(f =>
			{
				var positionY = position.y;
				var textTrans = f.transform.Find("Text");
				var textComop = textTrans.GetComponent<UnityEngine.UI.Text>();
				textComop.text = text;
				if (critical)
				{
					textComop.color = Color.red;
				}
				ActionKit.Sequence().Lerp(0,0.5f,0.5f,(p)=>
				{
					f.PositionY(positionY + p*0.25f);
					textComop.LocalScaleX(Mathf.Clamp01(p *8));
					textComop.LocalScaleY(Mathf.Clamp01(p *8));
				},()=>{
					
				}).Delay(0.5f).Lerp(1f,0,0.3f,p=>
				{
					textComop.ColorAlpha(p);
				},()=>
				{
					f.DestroyGameObjGracefully();
					//textTrans.DestroyGameObjGracefully();
				}).Start(textComop);
			}).Show();
		}
	}
}
