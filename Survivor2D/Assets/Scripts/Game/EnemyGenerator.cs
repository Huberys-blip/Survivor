using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;
using ProjectSurvicor;

namespace Script
{
	[Serializable]
	public class EnemyWave
	{
		public float GenerateDuration;
		public GameObject EnemyPrefab;
		public int Seconds = 10;
	}
	public partial class EnemyGenerator : ViewController
	{
		private float mCurrenGenerateSecond = 0f;
		private float mCurrentWaveSeconds = 0f;
		public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();
		/// <summary>
		/// 队列化敌人波次
		/// </summary>
		public Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int WaveCont = 0;
		public bool LastWave => WaveCont == enemyWaves.Count;
		public EnemyWave CurrentWave => mCurrentWave;
		private void Start()
		{
			Global.ResetData();
			foreach (var item in enemyWaves)
			{
				enemyWavesQueue.Enqueue(item);
			}
		}
		public EnemyWave mCurrentWave = null;
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
						var randomAngle = UnityEngine.Random.Range(0f, 360f);
						var randomRadius = randomAngle * Mathf.Deg2Rad;
						var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
						var ganeratePos = player.transform.position + direction * 10f;
						if (mCurrentWave.EnemyPrefab != null)
						{
							mCurrentWave.EnemyPrefab.Instantiate()
					.Position(ganeratePos)
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
