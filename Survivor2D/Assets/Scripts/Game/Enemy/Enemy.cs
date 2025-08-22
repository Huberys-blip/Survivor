using UnityEngine;
using QFramework;
using ProjectSurvicor;
using QAssetBundle;

namespace Script
{
	public partial class Enemy : ViewController,IEnemy
	{
		public float Hp = 3f;
		public float MoveSpeed = 2f;
		public Color DissolveColor = Color.red;
		void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
		}
		void OnDestroy()
		{
			EnemyGenerator.EnemyCount.Value--;
		}
		void FixedUpdate()
		{
			if (Player.Instance)
			{
				var direction = (Player.Instance.transform.position - transform.position).normalized;
				SelfRigidbody2D.velocity = direction * MoveSpeed;
			}
			else
			{
				SelfRigidbody2D.velocity = Vector2.zero;
			}
        }
        void Update()
		{	
			if (Hp <= 0)
			{
				Global.GeneratePowerUp(gameObject);
				FxController.Play(Sprite,DissolveColor);
				AudioKit.PlaySound(Sfx.ENEMYDIE);
				this.DestroyGameObjGracefully();	
			}
		}
		private bool mIgnreHurt = false;
		public void Hurt(float damage)
		{
			if (mIgnreHurt)return;
			FloatingTextController.Play(transform.position, damage.ToString("0"));
			AudioKit.PlaySound("Hit");
			Sprite.color = Color.red;
			ActionKit.Delay(0.2f, () =>
			{
				//"简单能力".LogInfo();
				this.Hp -= damage;
				this.Sprite.color = Color.white;
				mIgnreHurt = false;
			}).Start(this);
		}

        public void SetSpeedScale(float speedScale)
        {
			MoveSpeed *= speedScale;
        }

        public void SetHpScale(float hpScale)
        {
			Hp *= hpScale;
        }
    }
}
