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
        public static BindableProperty<int> hp = new(3);
        /// <summary>
        /// 主角最大hp
        /// </summary>
        public static BindableProperty<int> MaxHp = new(3);
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new(0);
        /// <summary>
        /// 金币
        /// </summary>
        public static BindableProperty<int> Coin = new(0);
        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new(1);
        /// <summary>
        /// 简单能力伤害
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new(Config.InitSimpleSwordDamage);
        /// <summary>
        /// 简单能力间隔
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new(Config.InitSimpleSwordDuration);
        /// <summary>
        /// 简单能力攻击数量
        /// </summary>
        public static BindableProperty<int> SimpleSwordConut = new(Config.InitSimpleSwordCount);
        /// <summary>
        /// 范围
        /// </summary>
        public static BindableProperty<float> SimpleSwordRange= new(Config.InitSimpleSwordRange);
        /// <summary>
        /// 小刀伤害
        /// </summary>
        public static BindableProperty<float> SimpleKnifeDamage = new(Config.InitSimpleKnifeDamage);
        /// <summary>
        /// 小刀间隔
        /// </summary>
        public static BindableProperty<float> SimpleKnifeDuration = new(Config.InitSimpleKnifeDuration);
        /// <summary>
        /// 小刀数量
        /// </summary>
        public static BindableProperty<int> SimpleKnifeCount = new(Config.InitSimpleKnifeCount);
        /// <summary>
        ///  守护伤害
        /// </summary>
        public static BindableProperty<float> RotateSwordDamage = new(Config.InitRotateSwordDamage);
        /// <summary>
        ///  守护数量
        /// </summary>
        public static BindableProperty<int> RotateSwordCount = new(Config.InitRotateSwordCount);
        /// <summary>
        ///  守护速度
        /// </summary>
        public static BindableProperty<float> RotateSwordSpeed = new(Config.InitRotateSwordSpeed);
        /// <summary>
        ///  守护范围
        /// </summary>
        public static BindableProperty<float> RotateSwordRange = new(Config.InitRotateSwordRange);
     
        /// <summary>
        /// 间隔时间
        /// </summary>
        public static BindableProperty<float> CuhrrentSeconds = new(0f);
        /// <summary>
        /// 经验掉落概率
        /// </summary>
        public static BindableProperty<float> ExpPercent = new(0.4f);
        /// <summary>
        /// 金币掉落概率
        /// </summary>
        public static BindableProperty<float> CoinPercent = new(0.4f);
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
            SimpleAbilityDamage.Value = Config.InitSimpleSwordDamage;
            SimpleAbilityDuration.Value = Config.InitSimpleSwordDuration;
            SimpleSwordConut.Value = Config.InitSimpleSwordCount;
            CuhrrentSeconds.Value = 0f;
            SimpleSwordRange.Value = Config.InitSimpleSwordRange;
            EnemyGenerator.EnemyCount.Value = 0;
            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
            SimpleKnifeDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKnifeDuration.Value = Config.InitSimpleKnifeDuration;
            SimpleKnifeCount.Value = Config.InitSimpleKnifeCount;
            RotateSwordDamage.Value = Config.InitRotateSwordDamage;
            RotateSwordCount.Value = Config.InitRotateSwordCount;
            RotateSwordSpeed.Value = Config.InitRotateSwordSpeed;
            RotateSwordRange.Value = Config.InitRotateSwordRange;
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
             var magnetrandom = UnityEngine.Random.Range(0, 1f);
               if (magnetrandom <= 0.3f)
            {
                PowerUpManager.Instance.Magnet.Instantiate()
                   .Position(enemy.Position())
                   .Show();
            }
        }

        protected override void Init()
        {
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
        }
    }
}

