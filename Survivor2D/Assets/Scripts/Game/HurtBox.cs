using UnityEngine;
using QFramework;

namespace Script
{
	public partial class HurtBox : ViewController
	{
		public GameObject Owner;
		void Start()
		{
			if (!Owner)
			{
				Owner =transform.parent.gameObject;
			}
		}
	}
}
