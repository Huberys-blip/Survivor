using UnityEngine;
using QFramework;
using ProjectSurvicor;
using QAssetBundle;

namespace Script
{
	
	public partial class Player : ViewController
	{
		public float MoveSpeed = 5f;
		public static Player Instance;
		private AudioPlayer mWalksfx;
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
			HitBox.OnTriggerEnter2DEvent(Collider2D =>
			{
				var hitBox = Collider2D.GetComponent<HitBox>();
				if (hitBox != null)
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
			void UpdateHp()
			{
					HpValue.fillAmount = Global.hp.Value / (float)Global.MaxHp.Value;
			}
			Global.hp.RegisterWithInitValue(hp =>
			{
				UpdateHp();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.MaxHp.RegisterWithInitValue(maxhp=>
			{
				UpdateHp();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		bool mFaceRight = true;
		void Update()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var targetVelocity = new Vector2(horizontal, vertical).normalized *( MoveSpeed*Global.MovementSpeedRate.Value);

			if (horizontal == 0 && vertical == 0)
			{
				if (mFaceRight)
				{
					Sprite.Play("PlayerIdeRight");
				}
				else
				{
					Sprite.Play("PlayerIdeLeft");
				}
				if (mWalksfx != null)
				{
					mWalksfx.Stop();
					mWalksfx = null;
				}
			}
			else
			{
				if (mWalksfx==null)
				{
					mWalksfx = AudioKit.PlaySound(Sfx.WALK,true);
				}
				if (horizontal > 0)
					{
						mFaceRight = true;
					}
					else if (horizontal < 0)
					{
						mFaceRight = false;
					}
				if (mFaceRight)
				{
					Sprite.Play("PlayerWalkRight");
				}
				else
				{
					Sprite.Play("PlayerWalkLeft");
				}
			}

			MyRigidbody2D.velocity = Vector2.Lerp(MyRigidbody2D.velocity, targetVelocity,1-Mathf.Exp(-Time.deltaTime * 5));
		
		}
	}
}
