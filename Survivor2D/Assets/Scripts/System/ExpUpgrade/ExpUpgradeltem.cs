using System;
using QFramework;
using UnityEngine;

namespace ProjectSurvicor
{
    public class ExpUpgradeltem
    {
        public EasyEvent OnChanged = new();
        public bool UpgradeFinish { get; set; } = false;
        public string Key { get; private set; }
        public string Descriptopn => mDescriprionFactory(CurrentLeve.Value);
        public int MaxLevel { get; private set; }
        public BindableProperty<int> CurrentLeve =new (1);
        public BindableProperty<bool> Visible = new();
        private Func<int, string> mDescriprionFactory;
        public void Upgrade()
        {
            CurrentLeve.Value++;
            mOnUpgrade?.Invoke(this, CurrentLeve.Value);
            if (CurrentLeve.Value > 10)
            {
                UpgradeFinish = true;
            }
            OnChanged.Trigger();
        }
        private Action<ExpUpgradeltem,int> mOnUpgrade;

        public ExpUpgradeltem WihKey(string key)
        {
            Key = key;
            return this;
        }
        public ExpUpgradeltem WithDescription(Func<int,string> descriptionFactoey)
        {
            mDescriprionFactory = descriptionFactoey;
            return this;
        }
        public ExpUpgradeltem OnUpgrade(Action<ExpUpgradeltem,int> monUpgrade)
        {
            mOnUpgrade = monUpgrade;
            return this;
        }
         public ExpUpgradeltem WithMaxLevel(int maxLevel)
        {
            MaxLevel = maxLevel;
            return this;
        }
    }
    
}