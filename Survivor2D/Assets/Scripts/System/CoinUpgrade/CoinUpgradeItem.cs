using System;
using QFramework;

namespace ProjectSurvicor
{
    public class CoinUpgradeItem
    {
        public EasyEvent OnChanged = new();
        public bool UpgradeFinish { get; set; } = false;
        public string Key { get; private set; }
        public string Descriptopn { get; private set; }
        public int Price { get; private set; }
        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            OnChanged.Trigger();
            UpgradeFinish = true;
            CoinUpgradeSystem.OnColinUpgradesystemChanged.Trigger();
        }
        public bool CounditionCheck()
        {
            if (mCondition != null)
            {
                return !UpgradeFinish&&mCondition.Invoke(this);
            }
            return  !UpgradeFinish;
        }
        private Action<CoinUpgradeItem> mOnUpgrade;
        private Func<CoinUpgradeItem, bool> mCondition;
        public CoinUpgradeItem WihKey(string key)
        {
            Key = key;
            return this;
        }
        public CoinUpgradeItem WithDescription(string description)
        {
            Descriptopn = description;
            return this;
        }

        public CoinUpgradeItem WithPrice(int price)
        {
            Price = price;
            return this;
        }
        public CoinUpgradeItem OnUpgrade(Action<CoinUpgradeItem> monUpgrade)
        {
            mOnUpgrade = monUpgrade;
            return this;
        }
        public CoinUpgradeItem Condition(Func<CoinUpgradeItem,bool> condition)
        {
            mCondition = condition;
            return this;
        }
    }
}