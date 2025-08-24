using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using ProjectSurvicor;

namespace Script
{
	public class UIGamePasspanelData : UIPanelData
	{
	}
	public partial class UIGamePasspanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePasspanelData ?? new UIGamePasspanelData();
			Time.timeScale = 0;
			// ActionKit.OnUpdate.Register(() =>
			// {
			// 	if (Input.GetKeyDown(KeyCode.Space))
			// 	{
			// 		Global.ResetData();
			// 		this.CloseSelf();
			// 		SceneManager.LoadScene("Game");
			// 	}
			// }).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnBackToStart.onClick.AddListener(() =>
		{
			this.CloseSelf();
			SceneManager.LoadScene("GameStart");
		});
			AudioKit.PlaySound("GamePoss");
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
			Time.timeScale = 1;
		}
	}
}
