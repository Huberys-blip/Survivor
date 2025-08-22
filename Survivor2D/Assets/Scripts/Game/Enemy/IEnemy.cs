namespace ProjectSurvicor
{
    public interface IEnemy
    {
        void Hurt(float damage);
        void SetSpeedScale( float speedScale);
        void SetHpScale(float hpScale);
    }
}