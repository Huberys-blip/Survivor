using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;
namespace ProjectSurvicor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeltem> items { get; } = new List<ExpUpgradeltem>();
        public Dictionary<string, ExpUpgradeltem> Dictionary = new();
        public Dictionary<string, string> Pairs = new()
        {
            {"simple_sword","simple_critical"},
            {"simple_bomb","simple_fly_count"},
            {"simple_knife","damage_rate"},
            {"basket_ball","movement_speed_rate"},
            {"rotate_sword","simple_exp"},

            { "simple_critical","simple_sword"},
            {"simple_fly_count","simple_bomb"},
            {"damage_rate","simple_knife"},
            {"movement_speed_rate","basket_ball"},
            {"simple_exp","rotate_sword"},
        };
        public Dictionary<string, BindableProperty<bool>> PairedProperties = new()
        {
            {"simple_sword",Global.SuperSword},
            {"simple_bomb",Global.SuperBomb},
            {"simple_knife",Global.SuperKnife},
            {"basket_ball",Global.SuperBasketBall},
            {"rotate_sword",Global.SuperRotateSword}
        };
        public ExpUpgradeltem Add(ExpUpgradeltem item)
        {
            items.Add(item);
            return item;
        }
        protected override void OnInit()
        {
            ResetData();
            Global.Level.Register((_) =>
           {
               Roll();
           });
        }

        public void ResetData()
        {
            items.Clear();

            Add(new ExpUpgradeltem(true)
            .WihKey("simple_sword")
             .WithName("剑")
             .WithIconName("simple_sword_icon")
             .WithPairedaName("合成后的剑")
             .WithpairedIconName("paired_simple_sword_icon")
             .WithPairedDescription("攻击力翻倍 攻击范围翻倍")
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"剑LV{lv}:\n攻击身边的敌人",
                    2 => $"剑LV{lv}:\n攻击力+3 数量+2",
                    3 => $"剑LV{lv}:\n攻击力+2 间隔-0.25s",
                    4 => $"剑LV{lv}:\n攻击力+2 间隔-0.25s",
                    5 => $"剑LV{lv}:\n攻击力+3 数量+2",
                    6 => $"剑LV{lv}:\n攻击力+1 间隔-0.25s",
                    7 => $"剑LV{lv}:\n攻击力+3 数量+2",
                    8 => $"剑LV{lv}:\n攻击力+2 范围+1",
                    9 => $"剑LV{lv}:\n攻击力+3 间隔-0.25s",
                    10 => $"剑LV{lv}:\n攻击力+3 数量+2",
                    _ => null
                };
            })
            .WithMaxLevel(10)
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.SimpleSwordUnlocked.Value = true;
                        break;
                    case 2:
                        Global.SimpleAbilityDamage.Value += 3;
                        Global.SimpleSwordConut.Value += 2;
                        break;
                    case 3:
                        Global.SimpleAbilityDamage.Value += 2;
                        Global.SimpleAbilityDuration.Value -= 0.25f;
                        break;
                    case 4:
                        Global.SimpleAbilityDamage.Value += 2;
                        Global.SimpleAbilityDuration.Value -= 0.25f;
                        break;
                    case 5:
                        Global.SimpleAbilityDamage.Value += 3;
                        Global.SimpleSwordConut.Value += 2;
                        break;
                    case 6:
                        Global.SimpleSwordRange.Value++;
                        Global.SimpleAbilityDuration.Value -= 0.25f;
                        break;
                    case 7:
                        Global.SimpleAbilityDamage.Value += 3;
                        Global.SimpleSwordConut.Value += 2;
                        break;
                    case 8:
                        Global.SimpleAbilityDamage.Value += 2;
                        Global.SimpleSwordRange.Value++;
                        break;
                    case 9:
                        Global.SimpleAbilityDamage.Value += 3;
                        Global.SimpleAbilityDuration.Value -= 0.25f;
                        break;
                    case 10:
                        Global.SimpleAbilityDamage.Value += 3;
                        Global.SimpleSwordConut.Value += 2;
                        break;
                }
            })
            );

            Add(new ExpUpgradeltem(false).WihKey("simple_bomb")
                   .WithName("炸弹")
             .WithIconName("bomb_icon")
             .WithPairedaName("合成后的炸弹")
             .WithpairedIconName("paired_bomb_icon")
             .WithPairedDescription("每隔15秒 爆炸一次")
           .WithMaxLevel(10)
           .WithDescription(lv =>
           {
               return lv switch
               {
                   1 => $"炸弹LV{lv}:\n攻击全部敌人(怪物掉落)",
                   2 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   3 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   4 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   5 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   6 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   7 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   8 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   9 => $"炸弹LV{lv}:\n掉落概率+5% 攻击+5",
                   10 => $"炸弹LV{lv}:\n掉落概率+10% 攻击+10",
                   _ => null
               };
           })
           .OnUpgrade((_, level) =>
           {
               switch (level)
               {
                   case 1:
                       Global.BombUnlocked.Value = true;
                       break;
                   case 2:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 3:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 4:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 5:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 6:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 7:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 8:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 9:
                       Global.BombDamage.Value += 5;
                       Global.BombPercent.Value += 0.05f;
                       break;
                   case 10:
                       Global.BombDamage.Value += 10;
                       Global.BombPercent.Value += 0.1f;
                       break;
               }
           })
           );




            Add(new ExpUpgradeltem(true).WihKey("simple_knife")
                  .WithName("飞刀")
             .WithIconName("simple_knife_icon")
             .WithPairedaName("合成后的飞刀")
             .WithpairedIconName("paired_simple_knife_icon")
             .WithPairedDescription("攻击力翻倍")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"飞刀LV{lv}:\n向最近的敌人发射一把飞刀",
                    2 => $"飞刀LV{lv}:\n攻击力+3 数量+2",
                    3 => $"飞刀LV{lv}:\n攻击力+1 间隔-0.1s 数量+1",
                    4 => $"飞刀LV{lv}:\n 间隔-0.1s 数量+1",
                    5 => $"飞刀LV{lv}:\n攻击力+3 数量+1",
                    6 => $"飞刀LV{lv}:\n间隔-0.1s 数量+1",
                    7 => $"飞刀LV{lv}:\n间隔-0.1s 数量+1",
                    8 => $"飞刀LV{lv}:\n攻击力+3 数量+1",
                    9 => $"飞刀LV{lv}:\n 间隔-0.1s 数量+1",
                    10 => $"飞刀LV{lv}:\n攻击力+3 数量+1",
                    _ => null
                };
            })
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.SimpleKnifeUnlocked.Value = true;
                        break;
                    case 2:
                        Global.SimpleKnifeDamage.Value += 3;
                        Global.SimpleKnifeCount.Value += 2;
                        break;
                    case 3:
                        Global.SimpleKnifeDamage.Value += 1;
                        Global.SimpleKnifeDuration.Value -= 0.1f;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 4:
                        Global.SimpleKnifeDuration.Value -= 0.1f;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 5:
                        Global.SimpleKnifeDamage.Value += 3;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 6:
                        Global.SimpleKnifeDuration.Value -= 0.1f;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 7:
                        Global.SimpleKnifeDuration.Value -= 0.1f;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 8:
                        Global.SimpleKnifeDamage.Value += 3;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 9:
                        Global.SimpleKnifeDuration.Value -= 0.1f;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                    case 10:
                        Global.SimpleKnifeDamage.Value += 3;
                        Global.SimpleKnifeCount.Value += 1;
                        break;
                }
            })
            );


            Add(new ExpUpgradeltem(true).WihKey("rotate_sword")
          .WithMaxLevel(10)
            .WithName("守护剑")
             .WithIconName("rotate_sword_icon")
             .WithPairedaName("合成后的守护剑")
             .WithpairedIconName("paired_rotate_sword_icon")
             .WithPairedDescription("攻击力翻倍 旋转速度翻倍")
          .WithDescription(lv =>
          {
              return lv switch
              {
                  1 => $"守护剑LV{lv}:\n环绕玩家身边的剑",
                  2 => $"守护剑LV{lv}:\n攻击力+1 数量+1",
                  3 => $"守护剑LV{lv}:\n攻击力+2 速度+25%",
                  4 => $"守护剑LV{lv}:\n速度+50%",
                  5 => $"守护剑LV{lv}:\n攻击力+1 数量+1",
                  6 => $"守护剑LV{lv}:\n攻击力+2 速度+25%",
                  7 => $"守护剑LV{lv}:\n攻击力+1 数量+1",
                  8 => $"守护剑LV{lv}:\n攻击力+2 速度+25%",
                  9 => $"守护剑LV{lv}:\n 攻击力+1 数量+1",
                  10 => $"守护剑LV{lv}:\n攻击力+2 速度+25%",
                  _ => null
              };
          })
          .OnUpgrade((_, level) =>
          {
              switch (level)
              {
                  case 1:
                      Global.RotateSwordUnlocked.Value = true;
                      break;
                  case 2:
                      Global.RotateSwordDamage.Value += 1;
                      Global.RotateSwordCount.Value += 1;
                      break;
                  case 3:
                      Global.RotateSwordDamage.Value += 2;
                      Global.RotateSwordSpeed.Value *= 1.25f;
                      break;
                  case 4:
                      Global.RotateSwordSpeed.Value *= 1.50f;
                      break;
                  case 5:
                      Global.RotateSwordDamage.Value += 1;
                      Global.RotateSwordCount.Value += 1;
                      break;
                  case 6:
                      Global.RotateSwordDamage.Value += 2;
                      Global.RotateSwordSpeed.Value *= 1.25f;
                      break;
                  case 7:
                      Global.RotateSwordDamage.Value += 1;
                      Global.RotateSwordCount.Value += 1;
                      break;
                  case 8:
                      Global.RotateSwordDamage.Value += 2;
                      Global.RotateSwordSpeed.Value *= 1.25f;
                      break;
                  case 9:
                      Global.RotateSwordDamage.Value += 1;
                      Global.RotateSwordCount.Value += 1;
                      break;
                  case 10:
                      Global.RotateSwordDamage.Value += 2;
                      Global.RotateSwordSpeed.Value *= 1.25f;
                      break;
              }
          })
          );



            Add(new ExpUpgradeltem(true).WihKey("basket_ball")
                  .WithName("篮球")
             .WithIconName("ball_icon")
             .WithPairedaName("合成后的篮球")
             .WithpairedIconName("paired_ball_icon")
             .WithPairedDescription("攻击力翻倍 体积变大")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"篮球LV{lv}:\n在屏幕内反弹的篮球",
                    2 => $"篮球LV{lv}:\n攻击力+3",
                    3 => $"篮球LV{lv}:\n数量+1",
                    4 => $"篮球LV{lv}:\n攻击力+3",
                    5 => $"篮球LV{lv}:\n数量+1",
                    6 => $"篮球LV{lv}:\n攻击力+3",
                    7 => $"篮球LV{lv}:\n速度+20%",
                    8 => $"篮球LV{lv}:\n攻击力+3",
                    9 => $"篮球LV{lv}:\n速度+20%",
                    10 => $"篮球LV{lv}:\n数量+1",
                    _ => null
                };
            })
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.BasketBallUnlocked.Value = true;
                        break;
                    case 2:
                        Global.BasketBallDamage.Value += 3;
                        break;
                    case 3:
                        Global.BasketBallCount.Value += 1;
                        break;
                    case 4:
                        Global.BasketBallDamage.Value += 3;
                        break;
                    case 5:
                        Global.BasketBallCount.Value += 1;
                        break;
                    case 6:
                        Global.BasketBallDamage.Value += 3;
                        break;
                    case 7:
                        Global.BasketBallSpeed.Value *= 1.2f;
                        break;
                    case 8:
                        Global.BasketBallDamage.Value += 3;
                        break;
                    case 9:
                        Global.BasketBallSpeed.Value *= 1.2f;
                        break;
                    case 10:
                        Global.BasketBallCount.Value += 1;
                        break;
                }
            })
            );



            Add(new ExpUpgradeltem(false).WihKey("simple_critical")
            .WithName("暴击")
            .WithIconName("critical_icon")
            .WithMaxLevel(11)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"暴击LV{lv}:\n每次伤害 8%暴击",
                    2 => $"暴击LV{lv}:\n每次伤害 16%暴击",
                    3 => $"暴击LV{lv}:\n每次伤害 24%暴击",
                    4 => $"暴击LV{lv}:\n每次伤害 32%暴击",
                    5 => $"暴击LV{lv}:\n每次伤害 40%暴击",
                    6 => $"暴击LV{lv}:\n每次伤害 48%暴击",
                    7 => $"暴击LV{lv}:\n每次伤害 56%暴击",
                    8 => $"暴击LV{lv}:\n每次伤害 64%暴击",
                    9 => $"暴击LV{lv}:\n每次伤害 72%暴击",
                    10 => $"暴击LV{lv}:\n每次伤害 80%暴击",
                    11 => $"暴击LV{lv}:\n每次伤害 88%暴击",
                    _ => null
                };
            })
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.CriticalRate.Value = 0.08f;
                        break;
                    case 2:
                        Global.CriticalRate.Value = 0.16f;
                        break;
                    case 3:
                        Global.CriticalRate.Value = 0.24f;
                        break;
                    case 4:
                        Global.CriticalRate.Value = 0.32f;
                        break;
                    case 5:
                        Global.CriticalRate.Value = 0.40f;
                        break;
                    case 6:
                        Global.CriticalRate.Value = 0.48f;
                        break;
                    case 7:
                        Global.CriticalRate.Value = 0.56f;
                        break;
                    case 8:
                        Global.CriticalRate.Value = 0.64f;
                        break;
                    case 9:
                        Global.CriticalRate.Value = 0.72f;
                        break;
                    case 10:
                        Global.CriticalRate.Value = 0.80f;
                        break;
                    case 11:
                        Global.CriticalRate.Value = 0.88f;
                        break;
                }
            })
            );

            Add(new ExpUpgradeltem(false).WihKey("damage_rate")
              .WithName("伤害率")
            .WithIconName("damage_icon")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"伤害率LV{lv}:\n每次伤害 10%暴击",
                    2 => $"伤害率LV{lv}:\n每次伤害 20%暴击",
                    3 => $"伤害率LV{lv}:\n每次伤害 30%暴击",
                    4 => $"伤害率LV{lv}:\n每次伤害 40%暴击",
                    5 => $"伤害率LV{lv}:\n每次伤害 50%暴击",
                    6 => $"伤害率LV{lv}:\n每次伤害 60%暴击",
                    7 => $"伤害率LV{lv}:\n每次伤害 70%暴击",
                    8 => $"伤害率LV{lv}:\n每次伤害 80%暴击",
                    9 => $"伤害率LV{lv}:\n每次伤害 90%暴击",
                    10 => $"伤害率LV{lv}:\n每次伤害 100%暴击",
                    _ => null
                };
            })
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.DamageRate.Value = 1.1f;
                        break;
                    case 2:
                        Global.DamageRate.Value = 1.2f;
                        break;
                    case 3:
                        Global.DamageRate.Value = 1.3f;
                        break;
                    case 4:
                        Global.DamageRate.Value = 1.4f;
                        break;
                    case 5:
                        Global.DamageRate.Value = 1.5f;
                        break;
                    case 6:
                        Global.DamageRate.Value = 1.6f;
                        break;
                    case 7:
                        Global.DamageRate.Value = 1.7f;
                        break;
                    case 8:
                        Global.DamageRate.Value = 1.8f;
                        break;
                    case 9:
                        Global.DamageRate.Value = 1.9f;
                        break;
                    case 10:
                        Global.DamageRate.Value = 2f;
                        break;
                }
            })
            );

            Add(new ExpUpgradeltem(false).WihKey("simple_fly_count")
              .WithName("飞行物")
            .WithIconName("fly_icon")
           .WithMaxLevel(4)
           .WithDescription(lv =>
           {
               return lv switch
               {
                   1 => $"飞行物LV{lv}:\n额外增加1个飞行物",
                   2 => $"飞行物LV{lv}:\n额外增加2个飞行物",
                   3 => $"飞行物LV{lv}:\n额外增加3个飞行物",
                   4 => $"飞行物LV{lv}:\n额外增加4个飞行物",
                   _ => null
               };
           })
           .OnUpgrade((_, level) =>
           {
               switch (level)
               {
                   case 1:
                       Global.AdditionalFlyThingCount.Value++;
                       break;
                   case 2:
                       Global.AdditionalFlyThingCount.Value++;
                       break;
                   case 3:
                       Global.AdditionalFlyThingCount.Value++;
                       break;
                   case 4:
                       Global.AdditionalFlyThingCount.Value++;
                       break;
               }
           })
           );


            Add(new ExpUpgradeltem(false).WihKey("movement_speed_rate")  .WithName("移动速度")
            .WithIconName("movement_icon")
       .WithMaxLevel(5)
       .WithDescription(lv =>
       {
           return lv switch
           {
               1 => $"移动速度LV{lv}:\n增加25%移动速度",
               2 => $"移动速度LV{lv}:\n增加50%移动速度",
               3 => $"移动速度LV{lv}:\n增加75%移动速度",
               4 => $"移动速度LV{lv}:\n增加100%移动速度",
               5 => $"移动速度LV{lv}:\n增加150%移动速度",
               _ => null
           };
       })
       .OnUpgrade((_, level) =>
       {
           switch (level)
           {
               case 1:
                   Global.MovementSpeedRate.Value = 1.25f;
                   break;
               case 2:
                   Global.MovementSpeedRate.Value = 1.5f;
                   break;
               case 3:
                   Global.MovementSpeedRate.Value = 1.75f;
                   break;
               case 4:
                   Global.MovementSpeedRate.Value = 2f;
                   break;
               case 5:
                   Global.MovementSpeedRate.Value = 2.5f;
                   break;
           }
       })
       );

            Add(new ExpUpgradeltem(false).WihKey("simple_collectable_area")
              .WithName("拾取范围")
            .WithIconName("collectable_icon")
               .WithMaxLevel(3)
               .WithDescription(lv =>
               {
                   return lv switch
                   {
                       1 => $"拾取范围LV{lv}:\n增加100%范围",
                       2 => $"拾取范围LV{lv}:\n增加200%范围",
                       3 => $"拾取范围LV{lv}:\n增加300%范围",
                       _ => null
                   };
               })
               .OnUpgrade((_, level) =>
               {
                   switch (level)
                   {
                       case 1:
                           Global.CollectableArea.Value = 2f;
                           break;
                       case 2:
                           Global.CollectableArea.Value = 3f;
                           break;
                       case 3:
                           Global.CollectableArea.Value = 4f;
                           break;
                   }
               })
               );

            Add(new ExpUpgradeltem(false).WihKey("simple_exp") 
             .WithName("经验值")
            .WithIconName("exp_icon")
                  .WithMaxLevel(5)
                  .WithDescription(lv =>
                  {
                      return lv switch
                      {
                          1 => $"经验值LV{lv}:\n增加5%范围",
                          2 => $"经验值LV{lv}:\n增加8%范围",
                          3 => $"经验值LV{lv}:\n增加12%范围",
                          4 => $"经验值LV{lv}:\n增加17%范围",
                          5 => $"经验值LV{lv}:\n增加25%范围",
                          _ => null
                      };
                  })
                  .OnUpgrade((_, level) =>
                  {
                      switch (level)
                      {
                          case 1:
                              Global.AdditonalExpercent.Value = 0.05f;
                              break;
                          case 2:
                              Global.AdditonalExpercent.Value = 0.08f;
                              break;
                          case 3:
                              Global.AdditonalExpercent.Value = 0.12f;
                              break;
                          case 4:
                              Global.AdditonalExpercent.Value = 0.17f;
                              break;
                          case 5:
                              Global.AdditonalExpercent.Value = 0.25f;
                              break;
                      }
                  })
                  );
            Dictionary = items.ToDictionary(i=>i.Key);
        }
        public void Roll()
        {
            foreach (var expitem in items)
            {
                expitem.Visible.Value = false;
            }
            var list = items.Where(item => !item.UpgradeFinish).ToList();

            if (list.Count >= 4)
            {
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else
            {
                foreach (var item in list)
                {
                    item.Visible.Value = true;
                }
            }
        }
    }
}