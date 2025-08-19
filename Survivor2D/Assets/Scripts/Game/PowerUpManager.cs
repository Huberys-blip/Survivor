using UnityEngine;
using QFramework;

namespace Script
{
	public partial class PowerUpManager : ViewController
	{
		public static PowerUpManager Instance;
		void Awake()
		{
			Instance = this;
		}
		void OnDestroy()
		{
			Instance = null;
		}
		void Start()
		{
			// Code Here
		}
	}
}
