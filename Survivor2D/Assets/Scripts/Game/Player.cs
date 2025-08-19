using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	
	public partial class Player : ViewController
	{
		public float MoveSpeed = 5f;
		public static Player Instance;
		void Awake()
		{
			Instance = this;
		}
        private void OnDestroy()
        {
            Instance = null;
        }
        void Start()
		{
			//"hello QFramework".LogInfo();
			HitBox.OnTriggerEnter2DEvent(Collider2D=>
			{
				var hitBox = Collider2D.GetComponent<HitBox>();
				if (hitBox!= null)
				{
						if (hitBox.Owner.CompareTag("Enemy"))
					{
						Global.hp.Value--;

						if (Global.hp.Value <= 0)
						{
							AudioKit.PlaySound("Die");
							this.DestroyGameObjGracefully();
							UIKit.OpenPanel<UIGameOverpanel>();
						}
						else
						{
							AudioKit.PlaySound("Huit");
						}

					}
				}
			
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		void Update()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var targetVelocity = new Vector2(horizontal, vertical).normalized * MoveSpeed;
			MyRigidbody2D.velocity = Vector2.Lerp(MyRigidbody2D.velocity, targetVelocity,1-Mathf.Exp(-Time.deltaTime * 5));
		
		}
	}
}
