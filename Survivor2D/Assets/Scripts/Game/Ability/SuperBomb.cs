using UnityEngine;
using QFramework;

namespace Script
{
	public partial class SuperBomb : ViewController
	{
		private float mCurrentSeconds = 0;
		void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= 15)
			{
				mCurrentSeconds = 0;
				Bomb.Execute();
			}
        }
    }
}
