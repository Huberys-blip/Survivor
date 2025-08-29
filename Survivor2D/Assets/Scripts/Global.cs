using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Script;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
namespace ProjectSurvicor
{
    public class Global : Architecture<Global>
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
        /// 简单能力开启
        /// </summary>
        public static BindableProperty<bool> SimpleSwordUnlocked = new(false);
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
        public static BindableProperty<float> SimpleSwordRange = new(Config.InitSimpleSwordRange);
        /// <summary>
        /// 小刀能力开启
        /// </summary>
        public static BindableProperty<bool> SimpleKnifeUnlocked = new(false);
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
        /// 守护能力开启
        /// </summary>
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);
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
        /// 篮球能力开启
        /// </summary>
        public static BindableProperty<bool> BasketBallUnlocked = new(false);
        /// <summary>
        /// 篮球伤害
        /// </summary>
        public static BindableProperty<float> BasketBallDamage = new(Config.InitBasketBallDamage);
        /// <summary>
        /// 篮球速度
        /// </summary>
        public static BindableProperty<float> BasketBallSpeed = new(Config.InitBasketBallSpeed);
        /// <summary>
        /// 篮球数量
        /// </summary>
        public static BindableProperty<int> BasketBallCount = new(Config.InitBasketBallCount);

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

        public static BindableProperty<bool> BombUnlocked = new(false);
        public static BindableProperty<float> BombDamage = new(Config.InitBombDamage);
        public static BindableProperty<float> BombPercent = new(Config.InitBombPercent);
        public static BindableProperty<float> CriticalRate = new(Config.InitCriticalRate);
        public static BindableProperty<float> DamageRate = new(1);
        public static BindableProperty<int> AdditionalFlyThingCount = new(0);
        public static BindableProperty<float> MovementSpeedRate = new(1);
        public static BindableProperty<float> CollectableArea = new(Config.InitCollectableArea);
        public static BindableProperty<float> AdditonalExpercent = new(0);

        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);
        public static BindableProperty<bool> SuperBasketBall = new(false);


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
            AdditonalExpercent.Value = 0;
            CollectableArea.Value = Config.InitCollectableArea;
            MovementSpeedRate.Value = 1;
            hp.Value = MaxHp.Value;
            AdditionalFlyThingCount.Value = 0;
            Exp.Value = 0;
            Level.Value = 1;
            DamageRate.Value = 1f;
            CuhrrentSeconds.Value = 0f;
            SimpleSwordRange.Value = Config.InitSimpleSwordRange;
            EnemyGenerator.EnemyCount.Value = 0;
            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
            SimpleAbilityDamage.Value = Config.InitSimpleSwordDamage;
            SimpleAbilityDuration.Value = Config.InitSimpleSwordDuration;
            SimpleSwordConut.Value = Config.InitSimpleSwordCount;
            SimpleKnifeDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKnifeDuration.Value = Config.InitSimpleKnifeDuration;
            SimpleKnifeCount.Value = Config.InitSimpleKnifeCount;
            RotateSwordDamage.Value = Config.InitRotateSwordDamage;
            RotateSwordCount.Value = Config.InitRotateSwordCount;
            RotateSwordSpeed.Value = Config.InitRotateSwordSpeed;
            RotateSwordRange.Value = Config.InitRotateSwordRange;
            BasketBallDamage.Value = Config.InitBasketBallDamage;
            BasketBallSpeed.Value = Config.InitBasketBallSpeed;
            BasketBallCount.Value = Config.InitBasketBallCount;
            SimpleSwordUnlocked.Value = false;
            SimpleKnifeUnlocked.Value = false;
            RotateSwordUnlocked.Value = false;
            BasketBallUnlocked.Value = false;
            BombUnlocked.Value = false;
            BombDamage.Value = Config.InitBombDamage;
            BombPercent.Value = Config.InitBombPercent;
            CriticalRate.Value = Config.InitCriticalRate;
            SuperKnife.Value = false;
            SuperSword.Value = false;
            SuperRotateSword.Value = false;
            SuperBomb.Value = false;
            SuperBasketBall.Value = false;
        }
        public static int ExpToNextLevel()
        {
            return Level.Value * 5;
        }
        /// <summary>
        ///经验掉落
        /// </summary>
        public static void GeneratePowerUp(GameObject enemy, bool treasureChes)
        {
            if (treasureChes)
            {
                PowerUpManager.Instance.TreasureChest
                .Instantiate()
                .Position(enemy.Position())
                .Show();
                return;
            }
            var exprandom = UnityEngine.Random.Range(0, 1f);
            if (exprandom <= ExpPercent.Value + AdditonalExpercent.Value)
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
            if (BombUnlocked.Value)
            {
                var bombrandom = UnityEngine.Random.Range(0, 1f);
                if (bombrandom <= BombPercent.Value)
                {
                    PowerUpManager.Instance.Bomb.Instantiate()
                    .Position(enemy.Position())
                    .Show();
                    return;
                }
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
            this.RegisterSystem(new AchievementSystem());
        }
    }
}

