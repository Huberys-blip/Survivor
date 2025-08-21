using System.Collections.Generic;
using System.Linq;
using QFramework;
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
             var simple_percent_LV1 = Add(new ExpUpgradeltem()
             .WihKey("simple_percent")
             .WithDescription(lv=>$"简单攻击力升级LV{lv}")
             .WithMaxLevel(10)
             .OnUpgrade((_, leve) =>
             {
                 if (leve == 1)
                 {

                 }
                 Global.SimpleAbilityDamage.Value *= 1.5f;
             }));





            var simple_duration_LV1 = Add(new ExpUpgradeltem()
            .WihKey("simple_duration")
            .WithDescription(lv=>$"简单攻击速度升级LV{lv}")
            .WithMaxLevel(10)
            .OnUpgrade((_, leve) =>
            {
                if (leve == 2)
                {

                }
                Global.SimpleAbilityDuration.Value *= 0.8f;
            }));

          
        }
        public void Roll()
        {
            foreach (var expitem in items)
            {
                expitem.Visible.Value = false;
            }
            var item = items.Where(item => !item.UpgradeFinish).ToList().GetRandomItem();
            if (item != null)
            {
                item.Visible.Value = true;
            }

        }
    }
}