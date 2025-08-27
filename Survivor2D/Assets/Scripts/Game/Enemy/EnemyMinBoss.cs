using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class EnemyMinBoss : ViewController, IEnemy
	{
		public enum States
		{
			/// <summary>
			/// 跟随玩家
			/// </summary>
			FlowingPlayer,
			/// <summary>
			/// 警戒
			/// </summary>
			Warning,
			/// <summary>
			/// 冲向玩家
			/// </summary>
			Dash,
			/// <summary>
			/// 等待
			/// </summary>
			Wait
		}
		public FSM<States> FSM = new();
		public float Hp = 50f;
		public float MoveSpeed = 2f;
		void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
			FSM.State(States.FlowingPlayer).OnFixedUpdate(()=>
			{
				if (Player.Instance)
				{
					var direction = (Player.Instance.transform.position - transform.position).normalized;
					SelfRigidbody2D.velocity = direction * MoveSpeed;
					if ((Player.Instance.Position()-transform.Position()).magnitude<=10)
					{
						FSM.StartState(States.Warning);	
					}
				}
				else
				{
					SelfRigidbody2D.velocity = Vector2.zero;
				}
			});
			FSM.State(States.Warning).OnEnter(() =>
			{
				SelfRigidbody2D.velocity = Vector2.zero;
			})
			.OnUpdate(() =>
			{
				var frames = 3 + (180 - FSM.FrameCountOfCurrentState) / 10;
				if (FSM.FrameCountOfCurrentState / frames % 2 == 0)
				{
					Sprite.color = Color.red;
				}
				else
				{
					Sprite.color = Color.blue;
				}
				if (FSM.FrameCountOfCurrentState >= 180)
				{
					Sprite.color = Color.white;
					FSM.StartState(States.Dash);
				}
			})
			.OnExit(() =>
			{
			    Debug.Log("OnExit未进去");
				Sprite.color = Color.white;
			});
			var dashStarPos = Vector3.zero;
			var dashStartDistanceToPlayer = 0f;
			FSM.State(States.Dash).OnEnter(() =>
			{
				var direction = (Player.Instance.Position() - transform.Position()).normalized;
				SelfRigidbody2D.velocity = direction * 15;
				dashStarPos = transform.Position();
				dashStartDistanceToPlayer =(Player.Instance.Position() - transform.Position()).magnitude;
			})
			.OnUpdate(()=>
			{
				var distance = (transform.Position()-dashStarPos).magnitude;
				if (distance >= dashStartDistanceToPlayer+6)
				{
					FSM.StartState(States.Wait);
				}
			});
			FSM.State(States.Wait).OnEnter(() =>
			{
				SelfRigidbody2D.velocity = Vector2.zero;
			})
			.OnUpdate(()=>
			{
				if (FSM.FrameCountOfCurrentState >= 30)
				{
					FSM.StartState(States.FlowingPlayer);
				}
			});
			FSM.StartState(States.FlowingPlayer);
		}
		void OnDestroy()
		{
			EnemyGenerator.EnemyCount.Value--;
		}
		void Update()
		{
			FSM.Update();
			if (Hp <= 0)
			{
				Global.GeneratePowerUp(gameObject,true);
				this.DestroyGameObjGracefully();
			}
		}
        private void FixedUpdate()
        {
			FSM.FixedUpdate();
        }
        private bool mIgnreHurt = false;
		public void Hurt(float damage,bool force=false,bool critical =false)
		{
			if (mIgnreHurt) return;
			FloatingTextController.Play(transform.position, damage.ToString("0"),critical);
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
