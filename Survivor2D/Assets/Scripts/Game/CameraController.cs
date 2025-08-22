using UnityEngine;
using QFramework;
using Unity.Burst.Intrinsics;
using UnityEngine.UIElements;

namespace Script
{
	public partial class CameraController : ViewController
	{
		private Vector2 mTargerposition = Vector2.zero;
		static CameraController Instance;
		public static Transform LBTrans => Instance.LB;
		public static Transform RTTrans => Instance.RT;
		private void Awake()
		{
			Instance = this;
		}
		private void OnDestroy()
		{
			Instance = null;
		}
		private void Start()
		{
			Application.targetFrameRate = 60;
		}
		private Vector3 mCurrentCameraPos;
		private bool mShake = false;
		private int mShakeFrame = 0;
		/// <summary>
		/// 震幅
		/// </summary>
		private float mShakeA = 0.5f;
		public static void Shake()
		{
			Instance.mShake = true;
			Instance.mShakeFrame = 30;
			Instance.mShakeA = 0.5f;
		}
		private void Update()
		{
			if (Player.Instance)
			{
				mTargerposition = Player.Instance.transform.position;
				mCurrentCameraPos.x = ((1 - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x, mTargerposition.x));
				mCurrentCameraPos.y = ((1 - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y, mTargerposition.y));
				mCurrentCameraPos.z = transform.position.z;
				if (mShake)
				{
					mShakeFrame--;
					var shakeA = Mathf.Lerp(mShakeA, 0f, (mShakeFrame / 30f));
					transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-shakeA, shakeA), mCurrentCameraPos.y + Random.Range(-shakeA, shakeA), mCurrentCameraPos.z);
					if (mShakeFrame <= 0)
					{
						mShake = false;
					}
				}
				else
				{
					transform.PositionX((1 - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x, mTargerposition.x));
					transform.PositionY((1 - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y, mTargerposition.y));
				}

			}
		}
	}
}
