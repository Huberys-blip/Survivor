using QFramework;
using Script;
using UnityEngine;

namespace ProjectSurvicor
{
    public abstract class PowerUp : GamePlayObject
    {
        public bool FlyingPlayer { get; set; }
       	public int FlayingToPlayerFrameCount = 0;
        protected abstract void Execute();
		private void Update()
        {
            if (FlyingPlayer)
            {
                if (FlayingToPlayerFrameCount == 0)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
                FlayingToPlayerFrameCount++;
                if (Player.Instance)
                {
                    var direction = Player.Instance.Direction2DFrom(this);
                    var distance = direction.magnitude;
                    if (FlayingToPlayerFrameCount <= 15)
                    {
                        transform.Translate(direction.normalized * -2 * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(direction.normalized * 7.5f * Time.deltaTime);
                    }
                    if (distance < 0.1f)
                    {
                        Execute();
                    }
                }
            }
        }
    }
}