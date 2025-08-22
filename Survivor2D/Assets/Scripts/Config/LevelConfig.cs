using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectSurvicor
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        public List<EnemyWaveGroup> enemyWaveGroups = new();
    }
    [Serializable]
    public class EnemyWaveGroup
    {
        public string Name;
         [SerializeField]
        [TextArea] public string Description = string.Empty;
        public List<EnemyWave> Waves = new();
    }
    [Serializable]
    public class EnemyWave
    {
        public string Name;
        public bool Actice = true;
        public float GenerateDuration;
        public GameObject EnemyPrefab;
        public int Seconds = 10;
        public float HpScale = 1;
        public float SpeedScale = 1;
	}
}

