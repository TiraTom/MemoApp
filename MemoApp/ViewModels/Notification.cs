using MemoApp.Models;
using MemoApp.Views;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using static MemoApp.Models.ConfigModel;

namespace MemoApp.ViewModels
{
	public class Notification
	{
		public ToastContent toastContent = new ToastContent()
		{
			Visual = new ToastVisual()
			{
				BindingGeneric = new ToastBindingGeneric()
				{
					Children =
					{
						new AdaptiveText()
						{
							Text = "MemoAppからのお知らせ"
						},

						new AdaptiveText()
						{
							Text = $"作業開始から{ConfigModel.GetSpecificConfigValue(ConfigType.NotificationSpanMinute)}分が経ちました。\r少し休憩をはさみましょう。"
						}
					}
				}
			},

			Launch = new QueryString()
			{
				{ "action", "MainPage.MoveMainPage" }
			}.ToString()
		};



		public void NotifyRestTime()
		{
			ToastNotification toast = new ToastNotification(toastContent.GetXml())
			{
				ExpirationTime = DateTime.Now.AddDays(0.25)
			};
			ToastNotificationManager.CreateToastNotifier().Show(toast);
		}
	}
}
