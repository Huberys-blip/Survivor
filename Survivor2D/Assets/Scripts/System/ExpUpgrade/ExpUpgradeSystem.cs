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

            Add(new ExpUpgradeltem()
            .WihKey("simp_sword")
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"剑LV{lv}:攻击身边的敌人",
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
            Add(new ExpUpgradeltem().WihKey("simple_knife")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"飞刀LV{lv}:向最近的敌人发射一把飞刀",
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
          .WithMaxLevel(10)
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
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


              Add(new ExpUpgradeltem().WihKey("rotate_sword")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"守护剑LV{lv}:环绕玩家身边的剑",
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
          .WithMaxLevel(10)
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        break;
                    case 2:
                         Global.RotateSwordDamage.Value +=1;
                         Global.RotateSwordCount.Value +=1;
                        break;
                    case 3:
                        Global.RotateSwordDamage.Value +=2;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 4:
                       Global.RotateSwordSpeed.Value *= 1.50f;
                        break;
                    case 5:
                         Global.RotateSwordDamage.Value +=1;
                         Global.RotateSwordCount.Value +=1;
                        break;
                    case 6:
                        Global.RotateSwordDamage.Value +=2;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 7:
                        Global.RotateSwordDamage.Value +=1;
                         Global.RotateSwordCount.Value +=1;
                        break;
                    case 8:
                        Global.RotateSwordDamage.Value +=2;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 9:
                         Global.RotateSwordDamage.Value +=1;
                         Global.RotateSwordCount.Value +=1;
                        break;
                    case 10:
                        Global.RotateSwordDamage.Value +=2;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                }
            })
            );
        }
        public void Roll()
        {
            foreach (var expitem in items)
            {
                expitem.Visible.Value = false;
            }
            foreach (var item in items.Where(item => !item.UpgradeFinish).Take(3))
            {
                if (item != null)
                {
                    item.Visible.Value = true;
                }
            }


        }
    }
}