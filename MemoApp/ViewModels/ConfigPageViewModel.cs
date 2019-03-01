using MemoApp.Models;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using static MemoApp.Models.ConfigModel;

namespace MemoApp.ViewModels
{
	public class ConfigPageViewModel
	{
		public string NotificationToggleLabel = "通知";
		public bool NotificationToggleValue { get; set; } = (GetSpecificConfigValue(ConfigType.NotificationFlag) == true.ToString());
		public string NotificationSpanMinuteLabel = "作業開始から以下の分が経過するごとに通知する";
		public string NotificationSpanMinuteValue { get; set; } = GetSpecificConfigValue(ConfigType.NotificationSpanMinute);
		public string UpdateButtonLabel = "更新";
		public async void UpdateConfig()
		{
			await UpdateSpecificConfig(ConfigType.NotificationFlag, NotificationToggleValue.ToString());
			await UpdateSpecificConfig(ConfigType.NotificationSpanMinute, NotificationSpanMinuteValue);
		}

	}
}
