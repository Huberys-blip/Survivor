using UnityEngine;

namespace ProjectSurvicor
{
    public class DamageSystem
    {
        public static void CalculatDamage(float basDamage, IEnemy enemy, int maxNormaLDamage = 2, float criticalDamageTime = 5)
        {
            basDamage *= Global.DamageRate.Value;
            if (Random.Range(0, 1.0f) < Global.CriticalRate.Value)
            {
                enemy.Hurt(basDamage + Random.Range(2f, criticalDamageTime), false, true);
            }
            else
            {
                enemy.Hurt(basDamage + Random.Range(-1, maxNormaLDamage));
            }
           
        }
    }
}