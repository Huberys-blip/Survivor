using UnityEngine;
using QFramework;
using System.Collections.Generic;
using ProjectSurvicor;
using System.Threading;

namespace Script
{
	public partial class BasketBallAility : ViewController
	{
		private List<Ball> mBalls = new();
		void Start()
		{
			void CreateBall()
			{
				mBalls.Add(Ball.Instantiate()
					.SyncPosition2DFrom(this)
					.Show());
			}
			void CreateBalls()
			{
	           var ballCount2Create = Global.BasketBallCount.Value + Global.AdditionalFlyThingCount.Value-mBalls.Count;
				for (int i = 0; i < ballCount2Create; i++)
				{
					CreateBall();
				}
			}
			Global.BasketBallCount.Or(Global.AdditionalFlyThingCount).Register(CreateBalls).UnRegisterWhenGameObjectDestroyed(gameObject);
			CreateBalls();
		}
	}
}
