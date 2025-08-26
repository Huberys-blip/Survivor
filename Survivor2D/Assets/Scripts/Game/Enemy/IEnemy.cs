namespace ProjectSurvicor
{
    public interface IEnemy
    {
        void Hurt(float damage,bool force =false ,bool critical =false);
        void SetSpeedScale( float speedScale);
        void SetHpScale(float hpScale);
    }
}