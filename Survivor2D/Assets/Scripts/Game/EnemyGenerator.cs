using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;
using ProjectSurvicor;

namespace Script
{
	public partial class EnemyGenerator : ViewController
	{
		[SerializeField]
		public LevelConfig Config;
		private float mCurrenGenerateSecond = 0f;
		private float mCurrentWaveSeconds = 0f;
		public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);
		/// <summary>
		/// 队列化敌人波次
		/// </summary>
		public Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int WaveCont = 0;
		private int mTotalCount = 0;
		public bool LastWave => WaveCont == mTotalCount;
		public EnemyWave CurrentWave => mCurrentWave;
		private void Start()
		{
			Global.ResetData();
			foreach (var group in Config.enemyWaveGroups)
			{
				foreach (var wave in group.Waves)
				{
					enemyWavesQueue.Enqueue(wave);
					mTotalCount++;
				}
			}
		}
		private EnemyWave mCurrentWave = null;
		private void Update()
		{
			if (mCurrentWave == null)
			{
				if (enemyWavesQueue.Count > 0)
				{
					WaveCont++;
					mCurrentWave = enemyWavesQueue.Dequeue();
					mCurrenGenerateSecond = 0f;
					mCurrentWaveSeconds = 0f;
				}
			}
			if (mCurrentWave != null)
			{
				mCurrenGenerateSecond += Time.deltaTime;
				mCurrentWaveSeconds += Time.deltaTime;
				if (mCurrenGenerateSecond >= mCurrentWave.GenerateDuration)
				{
					mCurrenGenerateSecond = 0f;
					var player = Player.Instance;
					if (player != null)
					{
						var xOry = RandomUtility.Choose(-1,1);
						var pos = Vector2.zero;
						if (xOry == -1)
						{
							pos.x = UnityEngine.Random.Range(CameraController.LBTrans.position.x, CameraController.RTTrans.position.x);
							pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y,CameraController.RTTrans.position.y);
						}
						else
						{
	  						pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x, CameraController.RTTrans.position.x);
							pos.y = UnityEngine.Random.Range(CameraController.LBTrans.position.y,CameraController.RTTrans.position.y);
						}
						if (mCurrentWave.EnemyPrefab != null)
						{
							mCurrentWave.EnemyPrefab.Instantiate()
							.Position(pos)
							.Self((self=>
							{
								var enemy = self.GetComponent<IEnemy>();
								enemy.SetSpeedScale(mCurrentWave.SpeedScale);
								enemy.SetHpScale(mCurrentWave.HpScale);
							}))
							.Show();
						}

					}
				}
				if (mCurrentWaveSeconds >= mCurrentWave.Seconds)
				{
					mCurrentWave = null;
				}
			}
		}
	}
}
