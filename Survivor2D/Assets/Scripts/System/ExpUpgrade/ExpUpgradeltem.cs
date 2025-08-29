using System;
using QFramework;
using UnityEngine;

namespace ProjectSurvicor
{
    public class ExpUpgradeltem
    {
        public ExpUpgradeltem(bool isweappn)
        {
            IsWeapon = isweappn;
        }
        /// <summary>
        /// 是否是武器
        /// </summary>
        public bool IsWeapon = false;
        public string Name { get; private set; }
        public EasyEvent OnChanged = new();
        public bool UpgradeFinish => CurrentLeve.Value>=MaxLevel;
        public string Key { get; private set; }
        public string Descriptopn => mDescriprionFactory(CurrentLeve.Value+1);
        public int MaxLevel { get; private set; }
        public string IconName { get; private set; }
        public BindableProperty<int> CurrentLeve = new(0);
        public BindableProperty<bool> Visible = new();
        private Func<int, string> mDescriprionFactory;
        public void Upgrade()
        {
            CurrentLeve.Value++;
            mOnUpgrade?.Invoke(this, CurrentLeve.Value);
            ExpUpgradeSystem.ChackAllUnlockedFinish();
            OnChanged.Trigger();
        }
        private Action<ExpUpgradeltem,int> mOnUpgrade;

        public  string PairedName { get; private set; }
        public string PairedDescription { get; private set; }
        public string PairedIconName { get; private set; }
        public ExpUpgradeltem WithPairedaName(string paoredName)
        {
            PairedName =paoredName;
            return this;
        }

        public ExpUpgradeltem WithPairedDescription (string pairedDescription)
        {
            PairedDescription = pairedDescription;
            return this;
        }
        public ExpUpgradeltem WithIconName (string iconName)
        {
            IconName = iconName;
            return this;
        }
        public ExpUpgradeltem WithpairedIconName(string pairedIconName)
        {
            PairedIconName =pairedIconName;
            return this;
        }
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
         public ExpUpgradeltem WithName(string name)
        {
            Name = name;
            return this;
        }
    }
    
}