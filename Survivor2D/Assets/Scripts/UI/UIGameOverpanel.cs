using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using ProjectSurvicor;

namespace Script
{
	public class UIGameOverpanelData : UIPanelData
	{
	}
	public partial class UIGameOverpanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameOverpanelData ?? new UIGameOverpanelData();
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Global.ResetData();
					this.CloseSelf();
					SceneManager.LoadScene("Game");
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnBackToStart.onClick.AddListener(() =>
			{
				this.CloseSelf();
				SceneManager.LoadScene("GameStart");
			});
		}
		private void Update()
		{

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

