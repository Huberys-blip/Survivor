using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Script;
using Unity.Mathematics;
using UnityEngine;
namespace ProjectSurvicor
{
    public class Global:Architecture<Global>
    {
        /// <summary>
        /// 主角hp
        /// </summary>
        public static BindableProperty<int> hp = new BindableProperty<int>(3);
        /// <summary>
        /// 主角最大hp
        /// </summary>
        public static BindableProperty<int> MaxHp = new BindableProperty<int>(3);
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        /// <summary>
        /// 金币
        /// </summary>
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        /// <summary>
        /// 简单能力伤害
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1f);
        /// <summary>
        /// 简单能力间隔
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);
        /// <summary>
        /// 间隔时间
        /// </summary>
        public static BindableProperty<float> CuhrrentSeconds = new BindableProperty<float>(0f);
        /// <summary>
        /// 经验掉落概率
        /// </summary>
        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.4f);
        /// <summary>
        /// 金币掉落概率
        /// </summary>
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.4f);
        [RuntimeInitializeOnLoadMethod]
        public static void Autoinit()
        {
            MaxHp.Value = PlayerPrefs.GetInt("Maxhp", 3);
            hp.Value = MaxHp.Value;
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);
            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt("coin", coin);
            });
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.4f);
            Global.ExpPercent.Register(expPercent =>
            {
                PlayerPrefs.SetFloat(nameof(ExpPercent), expPercent);
            });
            Global.CoinPercent.Register(coinPercent =>
            {
                PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent);
            });
            Global.MaxHp.Register(maxhp =>
            {
                PlayerPrefs.SetInt("Maxhp", maxhp);
            });
        }
        public static void ResetData()
        {
            hp.Value = MaxHp.Value;
            Exp.Value = 0;
            Level.Value = 1;
            SimpleAbilityDamage.Value = 1f;
            SimpleAbilityDuration.Value = 1.5f;
            CuhrrentSeconds.Value = 0f;
            EnemyGenerator.EnemyCount.Value = 0;
        }
        public static int ExpToNextLevel()
        {
            return Level.Value * 5;
        }
        /// <summary>
        ///经验掉落
        /// </summary>
        public static void GeneratePowerUp(GameObject enemy)
        {
            var exprandom = UnityEngine.Random.Range(0, 1f);
            if (exprandom <= ExpPercent.Value)
            {
                PowerUpManager.Instance.Exp.Instantiate()
                    .Position(enemy.Position())
                    .Show();
                return;
            }
            var coinrandom = UnityEngine.Random.Range(0, 1f);
            if (coinrandom <= ExpPercent.Value)
            {
                PowerUpManager.Instance.Coin.Instantiate()
                  .Position(enemy.Position())
                  .Show();
                return;
            }
            var hprandom = UnityEngine.Random.Range(0, 1f);
            if (hprandom <= 0.3f)
            {
                PowerUpManager.Instance.Hp.Instantiate()
                   .Position(enemy.Position())
                   .Show();
                return;
            }
            var bombrandom = UnityEngine.Random.Range(0, 1f);
            if (bombrandom <= 0.3f)
            {
                PowerUpManager.Instance.Bomb.Instantiate()
                   .Position(enemy.Position())
                   .Show();
                   return;
            }
              var getallexpbrandom = UnityEngine.Random.Range(0, 1f);
               if (getallexpbrandom <= 0.3f)
            {
                PowerUpManager.Instance.GetAllExp.Instantiate()
                   .Position(enemy.Position())
                   .Show();
            }
            // if (random <= 90)
            // {
            //     PowerUpManager.Instance.Exp.Instantiate()
            //         .Position(enemy.Position())
            //         .Show();
            // }
            // else
            // {
            //     PowerUpManager.Instance.Coin.Instantiate()
            //        .Position(enemy.Position())
            //        .Show();
            // }
        }

        protected override void Init()
        {
            this.RegisterSystem(new CoinUpgradeSystem());
        }
    }
}

