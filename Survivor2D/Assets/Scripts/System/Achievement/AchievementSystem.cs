using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;
namespace ProjectSurvicor
{
    public class AchievementSystem : AbstractSystem
    {
        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }
        public List<AchievementItem> Items = new();
        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new();

        protected override void OnInit()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            Add(new AchievementItem()
            .WithKey("3_mintes")
            .WithName("坚持三分钟")
            .WithDescriptione("坚持3分钟\n奖励1000金币")
            .WithIconName("achievement_time_icon")
            .Condition(() => Global.CuhrrentSeconds.Value >= 60 * 3)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
            Add(new AchievementItem()
            .WithKey("5_mintes")
            .WithName("坚持五分钟")
            .WithDescriptione("坚持5分钟\n奖励1000金币")
            .WithIconName("achievement_time_icon")
            .Condition(() => Global.CuhrrentSeconds.Value >= 60 * 5)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
             Add(new AchievementItem()
            .WithKey("10_mintes")
            .WithName("坚持十分钟")
            .WithDescriptione("坚持10分钟\n奖励1000金币")
            .WithIconName("achievement_time_icon")
            .Condition(() => Global.CuhrrentSeconds.Value >= 60 * 10)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
             Add(new AchievementItem()
            .WithKey("15_mintes")
            .WithName("坚持十五分钟")
            .WithDescriptione("坚持15分钟\n奖励1000金币")
            .WithIconName("achievement_time_icon")
            .Condition(() => Global.CuhrrentSeconds.Value >= 60 * 15)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
             Add(new AchievementItem()
            .WithKey("lv30")
            .WithName("30级")
            .WithDescriptione("第一次升级到30级\n奖励1000金币")
            .WithIconName("achievement_leve_icon")
            .Condition(() => Global.Level.Value >= 30)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
            Add(new AchievementItem()
            .WithKey("lv50")
            .WithName("50级")
            .WithDescriptione("第一次升级到50级\n奖励1000金币")
            .WithIconName("achievement_leve_icon")
            .Condition(() => Global.Level.Value >= 50)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
              Add(new AchievementItem()
            .WithKey("first_time_paired_ball")
            .WithName("合成后的篮球")
            .WithDescriptione("第一次解锁合成后的篮球\n奖励1000金币")
            .WithIconName("paired_simple_ball_icon")
            .Condition(() => Global.SuperBasketBall.Value)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
              Add(new AchievementItem()
            .WithKey("first_time_paired_bomb")
            .WithName("合成后的炸弹")
            .WithDescriptione("第一次解锁合成后的炸弹\n奖励1000金币")
            .WithIconName("paired_simple_bomb_icon")
            .Condition(() => Global.SuperBomb.Value)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
              Add(new AchievementItem()
            .WithKey("first_time_paired_sword")
            .WithName("合成后的剑")
            .WithDescriptione("第一次解锁合成后的剑\n奖励1000金币")
            .WithIconName("paired_simple_sword_icon")
            .Condition(() => Global.SuperSword.Value)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
               Add(new AchievementItem()
            .WithKey("first_time_paired_knife")
            .WithName("合成后的飞刀")
            .WithDescriptione("第一次解锁合成后的飞刀\n奖励1000金币")
            .WithIconName("paired_simple_kinfe_icon")
            .Condition(() => Global.SuperKnife.Value)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
               Add(new AchievementItem()
            .WithKey("first_time_paired_circle")
            .WithName("合成后的守卫剑")
            .WithDescriptione("第一次解锁合成后的守卫剑\n奖励1000金币")
            .WithIconName("paired_rotate_sword_icon")
            .Condition(() => Global.SuperRotateSword.Value)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);
            Add(new AchievementItem()
            .WithKey("first_time_paired_circle")
            .WithName("全部能力升级")
            .WithDescriptione("全部能力升级完成\n奖励1000金币")
            .WithIconName("achievement_all_icon")
            .Condition(() => ExpUpgradeSystem.AllUnlockedFinish)
            .WithmOnUnlocked(_ => { Global.Coin.Value += 1000; }))
            .Load(saveSystem);

            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(AchievementItem => !AchievementItem.Unlocked && AchievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);                        
                    }
                }
            });
        }
    }
}