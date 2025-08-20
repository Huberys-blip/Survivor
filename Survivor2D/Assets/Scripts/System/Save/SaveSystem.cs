using QFramework;
using UnityEngine;

namespace ProjectSurvicor
{
    public class SaveSystem :AbstractSystem
    {
        public void Save()
        {

        }
        public void Load()
        { 

        }
        public void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public void Saveint(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

          public int Loadint(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key,defaultValue);
        }

        
        public void Savestring(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

          public string Loadstring(string key, string defaultValue = default)
        {
            return PlayerPrefs.GetString(key,defaultValue);
        }

        protected override void OnInit()
        {

        }
    }
}