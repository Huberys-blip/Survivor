using UnityEngine;
using QFramework;

namespace Script
{
	public partial class GameUIController : ViewController
	{
		void Start()
		{
			UIKit.OpenPanel<UIGamepanel>();
		}
        private void OnDestroy()
        {
        	UIKit.ClosePanel<UIGamepanel>();
        }
    }
}
