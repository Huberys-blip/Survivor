using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class Enemy : ViewController,IEnemy
	{
		public float hp = 3f;
		public float MoveSpeed = 2f;
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
			if (hp <= 0)
			{
				Global.GeneratePowerUp(gameObject);
				this.DestroyGameObjGracefully();	
			}
		}
		private bool mIgnreHurt = false;
		public void Hurt(float damage)
		{
			if (mIgnreHurt)return;
			FloatingTextController.Play(transform.position, damage.ToString());
			AudioKit.PlaySound("Hit");
			Sprite.color = Color.red;
			ActionKit.Delay(0.2f, () =>
			{
				//"简单能力".LogInfo();
				this.hp -= damage;
				this.Sprite.color = Color.white;
				mIgnreHurt = false;
			}).Start(this);
		}
	}
}
