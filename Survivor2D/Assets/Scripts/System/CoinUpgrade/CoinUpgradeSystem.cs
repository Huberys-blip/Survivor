using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace ProjectSurvicor
{
    public class CoinUpgradeSystem : AbstractSystem,ICanSave
    {
        public static EasyEvent OnColinUpgradesystemChanged =new EasyEvent();
        public List<CoinUpgradeItem> items { get; } = new List<CoinUpgradeItem>();
        public CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            items.Add(item);
            return item;
        }
        protected override void OnInit()
        {

            var coinpercentLV1 = Add(new CoinUpgradeItem()
            .WihKey("coin_percent_LV1")
            .WithDescription("金币掉落概率升级LV1/")
            .WithPrice(5)
            .OnUpgrade((item) =>
            {
                Global.Coin.Value -= item.Price;
                Global.CoinPercent.Value += 0.1f;
            }));
            var coinpercentLV2 = Add(new CoinUpgradeItem()
          .WihKey("coin_percent_LV2")
          .WithDescription("金币掉落概率升级LV2/")
          .WithPrice(25)
          .Condition((_) => coinpercentLV1.UpgradeFinish)
          .OnUpgrade((item) =>
          {
              Global.Coin.Value -= item.Price;
              Global.CoinPercent.Value += 0.1f;
          }));
            coinpercentLV1.OnChanged.Register(() =>
            {
                coinpercentLV2.OnChanged.Trigger();
            });
            var coinpercentLV3 = Add(new CoinUpgradeItem()
           .WihKey("coin_percent_LV3")
           .WithDescription("金币掉落概率升级LV3/")
           .WithPrice(625)
           .Condition((_) => coinpercentLV1.UpgradeFinish)
           .OnUpgrade((item) =>
           {
               Global.Coin.Value -= item.Price;
               Global.CoinPercent.Value += 0.1f;
           }));
             coinpercentLV2.OnChanged.Register(() =>
            {
                coinpercentLV3.OnChanged.Trigger();
            });






            items.Add(new CoinUpgradeItem()
         .WihKey("exp_percent")
         .WithDescription("经验值掉落概率升级/")
         .WithPrice(5)
         .OnUpgrade((item) =>
         {
             Global.Coin.Value -= item.Price;
             Global.ExpPercent.Value += 0.1f;
         }));

            items.Add(new CoinUpgradeItem()
         .WihKey("Max_hp")
         .WithDescription("主角最大血量升级/")
         .WithPrice(30)
         .OnUpgrade((item) =>
         {
             Global.Coin.Value -= item.Price;
             Global.MaxHp.Value++;
         }));
            Load();
            OnColinUpgradesystemChanged.Register(()=>
            {
                Save();
            });
        }
        public void Say()
        {
            Debug.Log("CoinUpgradeSystem Say");
        }

        public void Save()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in items)
            {
                saveSystem.SaveBool(item.Key, item.UpgradeFinish);
               // PlayerPrefs.SetInt(item.Key, item.UpgradeFinish ? 1 : 0);
            }
        }

        public void Load()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in items)
            {
                item.UpgradeFinish =saveSystem.LoadBool(item.Key,false);
                //item.UpgradeFinish =PlayerPrefs.GetInt(item.Key,1)==1;
           }
        }
    }
}